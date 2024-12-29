# LIL - Logical Intermediate Language

temp:
```
@config
_silent=true
@code
ldstr Ciao, premi invio per proseguire
ldref System.Console
call WriteLine String
defass LILlo_Test
newobj LILlo_Test.Test
```

```
@settings
_ambient=1
@code
ldstr Ma porcaccio il dio
ldref System.Console
call WriteLine String
ldstr Allora, per favore inserisci la tua età
defvar user_eta
ldref System.Console
call WriteLine String
ldref System.Console
callvir ReadLine
convto Number
ldnum 18
ldop Grtoe
evalif 0x00
@rcp
0x00:
ldstr Sei maggiorenne!
ldref System.Console
call WriteLine String
```
