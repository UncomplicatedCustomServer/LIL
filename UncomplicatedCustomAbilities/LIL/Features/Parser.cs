using System;
using System.Collections.Generic;
using System.Linq;
using UncomplicatedCustomAbilities.LIL.Features.Script.Instructions;
using UncomplicatedCustomAbilities.LIL.Helpers;

namespace UncomplicatedCustomAbilities.LIL.Features
{
    public class Parser
    {
        public static Script.Script ParseGenericCode(string text, Script.Script script = null)
        {
            Instruction.Init();

            script ??= new();

            uint l = 0;
            foreach (string line in text.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries))
            {
                string rawopcode = (line.Contains(" ") ? line.Split(' ')[0] : line);
                string opcode = rawopcode.ToFirstCharUpper();
                if (!Enum.TryParse(opcode, out Enums.OpCodeType code))
                    throw new Exception($"Failed to parse OPCODE {opcode} into a real OpCodeType!");

                if (!Instruction.InstructionsAssociator.TryGetValue(code, out Type type))
                    throw new NotImplementedException($"Function related to OPCODE {code} ({opcode}) has not been implemented yet!");

                Instruction instruction = (Instruction)Activator.CreateInstance(type, [line.Contains(" ") ? line.Replace($"{rawopcode} ", "") : line.Replace(rawopcode, ""), script]);
                
                script.Instructions.Add(instruction);

                l++;
            }

            return script;
        }

        public static Script.Script ParseSettings(string text, Script.Script script = null)
        {
            script ??= new();

            foreach (string line in text.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries))
                script.GenericSettings.Add(line.Split('=').First(), line.Split('=').Last());

            return script;
        }

        public static Script.Script ParseReferencedCode(string text, Script.Script script = null)
        {
            script ??= new();

            Dictionary<int, List<string>> prc = [];
            int currentPrc = 0;
            foreach (string line in text.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.Length is 5 && line.ToCharArray().Last() is ':')
                    currentPrc = Convert.ToInt32(line.Replace(":", ""), 16);
                else
                {
                    if (prc.ContainsKey(currentPrc))
                        prc[currentPrc].Add(line);
                    else
                        prc.Add(currentPrc, [line]);
                }
            }

            foreach (KeyValuePair<int, List<string>> kvp in prc)
                script.Rcp.Add(kvp.Key, ParseGenericCode(string.Join(Environment.NewLine, kvp.Value)));

            return script;
        }

        public static Script.Script ParseRawFile(string text)
        {
            Script.Script script = new();

            Dictionary<string, List<string>> parts = [];
            string currentSection = string.Empty;

            foreach (string line in text.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.ToCharArray().First() is '@')
                    currentSection = line.Replace("@", "");
                else
                {
                    if (parts.ContainsKey(currentSection))
                        parts[currentSection].Add(line);
                    else
                        parts.Add(currentSection, [line]);
                }
            }

            if (parts.TryGetValue("settings", out List<string> settings))
                script = ParseSettings(string.Join(Environment.NewLine, settings), script);

            if (parts.TryGetValue("code", out List<string> code))
                script = ParseGenericCode(string.Join(Environment.NewLine, code), script);

            if (parts.TryGetValue("rcp", out List<string> rcp))
                script = ParseReferencedCode(string.Join(Environment.NewLine, rcp), script);

            return script;
        }
    }
}
