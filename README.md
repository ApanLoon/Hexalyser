# Hexalyser
An analyser for unknown file formats.

# Background
I had this idea sometime around 2001 and it has been kicking around in my mind ever since. In June of 2016 the idea had matured into a desired process of editing and I wrote the following example:

1: Open the file:

```xml
<ubyte numberOf="275">
    00000000: 5053 4430 0200 0000 0600 0000 6de7 fb3d  PSD0........m..=
    00000010: f628 9c3f cdcc 4c3e 0000 0040 f628 9c3f  .(.?..L>...@.(.?
    00000020: 1f85 6b3e 0000 9040 f628 9c3f cdcc 4c3e  ..k>...@.(.?..L>
    00000030: 6de7 fb3d f628 9c3f 0000 0040 0000 0040  m..=.(.?...@...@
    00000040: f628 9c3f 0000 0040 0000 8040 0000 0000  .(.?...@...@....
    00000050: 0000 0040 0200 0000 0000 0000 0000 2041  ...@.......... A
    00000060: 0300 0000 0873 5f67 7261 7373 0009 7269  .....s_grass..ri
    00000070: 6e74 6572 5f66 0003 0000 0000 0000 0004  nter_f..........
    00000080: 0000 0008 0000 0000 0000 0001 0002 0004  ................
    00000090: 0002 0003 0000 0050 0001 00b0 0002 0000  .......P........
    000000a0: 0003 0004 0001 0004 0000 0011 0000 0001  ................
    000000b0: 0001 0002 0000 0005 0000 0004 0001 0050  ...............P
    000000c0: 0001 00b0 0002 0001 0004 0005 0002 0050  ...............P
    000000d0: 0002 005e 0000 0001 0003 0001 0002 0005  ...^............
    000000e0: 0000 0404 cd00 0000 007a c400 007a c400  .........z...z..
    000000f0: 007a c400 007a 4400 007a 4400 007a 4400  .z...zD..zD..zD.
    00000100: 0000 0000 0000 0000 0000 0000 0000 0000  ................
    00000110: 0000 00                                  ...
</ubyte>
```

2: Locate a float, select the four bytes of the float and assign the "float" primitive:

```xml
<ubyte numberOf="12">
    00000000: 5053 4430 0200 0000 0600 0000            PSD0........    
</ubyte>
<float numberOf="1">0.123</float>
<ubyte numberOf="259">
    00000000: f628 9c3f cdcc 4c3e 0000 0040 f628 9c3f  .(.?..L>...@.(.?
    00000010: 1f85 6b3e 0000 9040 f628 9c3f cdcc 4c3e  ..k>...@.(.?..L>
    00000020: 6de7 fb3d f628 9c3f 0000 0040 0000 0040  m..=.(.?...@...@
    00000030: f628 9c3f 0000 0040 0000 8040 0000 0000  .(.?...@...@....
    00000040: 0000 0040 0200 0000 0000 0000 0000 2041  ...@.......... A
    00000050: 0300 0000 0873 5f67 7261 7373 0009 7269  .....s_grass..ri
    00000060: 6e74 6572 5f66 0003 0000 0000 0000 0004  nter_f..........
    00000070: 0000 0008 0000 0000 0000 0001 0002 0004  ................
    00000080: 0002 0003 0000 0050 0001 00b0 0002 0000  .......P........
    00000090: 0003 0004 0001 0004 0000 0011 0000 0001  ................
    000000a0: 0001 0002 0000 0005 0000 0004 0001 0050  ...............P
    000000b0: 0001 00b0 0002 0001 0004 0005 0002 0050  ...............P
    000000c0: 0002 005e 0000 0001 0003 0001 0002 0005  ...^............
    000000d0: 0000 0404 cd00 0000 007a c400 007a c400  .........z...z..
    000000e0: 007a c400 007a 4400 007a 4400 007a 4400  .z...zD..zD..zD.
    000000f0: 0000 0000 0000 0000 0000 0000 0000 0000  ................
    00000100: 0000 00                                  ...
</ubyte>
```

3: Name the float as "x" and repeat the process for the following two floats, naming them "y" and "z" respectively:

```xml
<ubyte numberOf="12">
    00000000: 5053 4430 0200 0000 0600 0000            PSD0........    
</ubyte>
<float numberOf="1" name="x">0.123</float>
<float numberOf="1" name="y">1.22</float>
<float numberOf="1" name="z">0.2</float>
<ubyte numberOf="251">
    00000000: 0000 0040 f628 9c3f 1f85 6b3e 0000 9040  ...@.(.?..k>...@
    00000010: f628 9c3f cdcc 4c3e 6de7 fb3d f628 9c3f  .(.?..L>m..=.(.?
    00000020: 0000 0040 0000 0040 f628 9c3f 0000 0040  ...@...@.(.?...@
    00000030: 0000 8040 0000 0000 0000 0040 0200 0000  ...@.......@....
    00000040: 0000 0000 0000 2041 0300 0000 0873 5f67  ...... A.....s_g
    00000050: 7261 7373 0009 7269 6e74 6572 5f66 0003  rass..rinter_f..
    00000060: 0000 0000 0000 0004 0000 0008 0000 0000  ................
    00000070: 0000 0001 0002 0004 0002 0003 0000 0050  ...............P
    00000080: 0001 00b0 0002 0000 0003 0004 0001 0004  ................
    00000090: 0000 0011 0000 0001 0001 0002 0000 0005  ................
    000000a0: 0000 0004 0001 0050 0001 00b0 0002 0001  .......P........
    000000b0: 0004 0005 0002 0050 0002 005e 0000 0001  .......P...^....
    000000c0: 0003 0001 0002 0005 0000 0404 cd00 0000  ................
    000000d0: 007a c400 007a c400 007a c400 007a 4400  .z...z...z...zD.
    000000e0: 007a 4400 007a 4400 0000 0000 0000 0000  .zD..zD.........
    000000f0: 0000 0000 0000 0000 0000 00              ...........
</ubyte>
```

4: Select the three floats and assign "structure", set the structure name to "Vertex":

```xml
<ubyte numberOf="12">
    00000000: 5053 4430 0200 0000 0600 0000            PSD0........    
</ubyte>
<struct_Vertex numberOf="1">
 <float numberOf="1" name="x">0.123</float>
 <float numberOf="1" name="y">1.22</float>
 <float numberOf="1" name="z">0.2</float>
</struct_Vertex>
<ubyte numberOf="251">
    00000000: 0000 0040 f628 9c3f 1f85 6b3e 0000 9040  ...@.(.?..k>...@
    00000010: f628 9c3f cdcc 4c3e 6de7 fb3d f628 9c3f  .(.?..L>m..=.(.?
    00000020: 0000 0040 0000 0040 f628 9c3f 0000 0040  ...@...@.(.?...@
    00000030: 0000 8040 0000 0000 0000 0040 0200 0000  ...@.......@....
    00000040: 0000 0000 0000 2041 0300 0000 0873 5f67  ...... A.....s_g
    00000050: 7261 7373 0009 7269 6e74 6572 5f66 0003  rass..rinter_f..
    00000060: 0000 0000 0000 0004 0000 0008 0000 0000  ................
    00000070: 0000 0001 0002 0004 0002 0003 0000 0050  ...............P
    00000080: 0001 00b0 0002 0000 0003 0004 0001 0004  ................
    00000090: 0000 0011 0000 0001 0001 0002 0000 0005  ................
    000000a0: 0000 0004 0001 0050 0001 00b0 0002 0001  .......P........
    000000b0: 0004 0005 0002 0050 0002 005e 0000 0001  .......P...^....
    000000c0: 0003 0001 0002 0005 0000 0404 cd00 0000  ................
    000000d0: 007a c400 007a c400 007a c400 007a 4400  .z...z...z...zD.
    000000e0: 007a 4400 007a 4400 0000 0000 0000 0000  .zD..zD.........
    000000f0: 0000 0000 0000 0000 0000 00              ...........
</ubyte>
```

5: Right-click the structure and select "Add presentation". In the window for list presentations, add this string: "(@x, @y, @z)" and select it as default. Also check the "one per row" check box:

```xml
<ubyte numberOf="12">
    00000000: 5053 4430 0200 0000 0600 0000            PSD0........    
</ubyte>
<struct_Vertex numberOf="1">
    (0.123, 1.22, 0.2)
</struct_Vertex>
<ubyte numberOf="251">
    00000000: 0000 0040 f628 9c3f 1f85 6b3e 0000 9040  ...@.(.?..k>...@
    00000010: f628 9c3f cdcc 4c3e 6de7 fb3d f628 9c3f  .(.?..L>m..=.(.?
    00000020: 0000 0040 0000 0040 f628 9c3f 0000 0040  ...@...@.(.?...@
    00000030: 0000 8040 0000 0000 0000 0040 0200 0000  ...@.......@....
    00000040: 0000 0000 0000 2041 0300 0000 0873 5f67  ...... A.....s_g
    00000050: 7261 7373 0009 7269 6e74 6572 5f66 0003  rass..rinter_f..
    00000060: 0000 0000 0000 0004 0000 0008 0000 0000  ................
    00000070: 0000 0001 0002 0004 0002 0003 0000 0050  ...............P
    00000080: 0001 00b0 0002 0000 0003 0004 0001 0004  ................
    00000090: 0000 0011 0000 0001 0001 0002 0000 0005  ................
    000000a0: 0000 0004 0001 0050 0001 00b0 0002 0001  .......P........
    000000b0: 0004 0005 0002 0050 0002 005e 0000 0001  .......P...^....
    000000c0: 0003 0001 0002 0005 0000 0404 cd00 0000  ................
    000000d0: 007a c400 007a c400 007a c400 007a 4400  .z...z...z...zD.
    000000e0: 007a 4400 007a 4400 0000 0000 0000 0000  .zD..zD.........
    000000f0: 0000 0000 0000 0000 0000 00              ...........
</ubyte>
```

6: Now select the four bytes before the structure and assign ulong, set the name of this value to "vertexCount":

```xml
<byte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="1">
    (0.123, 1.22, 0.2)
</struct_Vertex>
<ubyte numberOf="251">
    00000000: 0000 0040 f628 9c3f 1f85 6b3e 0000 9040  ...@.(.?..k>...@
    00000010: f628 9c3f cdcc 4c3e 6de7 fb3d f628 9c3f  .(.?..L>m..=.(.?
    00000020: 0000 0040 0000 0040 f628 9c3f 0000 0040  ...@...@.(.?...@
    00000030: 0000 8040 0000 0000 0000 0040 0200 0000  ...@.......@....
    00000040: 0000 0000 0000 2041 0300 0000 0873 5f67  ...... A.....s_g
    00000050: 7261 7373 0009 7269 6e74 6572 5f66 0003  rass..rinter_f..
    00000060: 0000 0000 0000 0004 0000 0008 0000 0000  ................
    00000070: 0000 0001 0002 0004 0002 0003 0000 0050  ...............P
    00000080: 0001 00b0 0002 0000 0003 0004 0001 0004  ................
    00000090: 0000 0011 0000 0001 0001 0002 0000 0005  ................
    000000a0: 0000 0004 0001 0050 0001 00b0 0002 0001  .......P........
    000000b0: 0004 0005 0002 0050 0002 005e 0000 0001  .......P...^....
    000000c0: 0003 0001 0002 0005 0000 0404 cd00 0000  ................
    000000d0: 007a c400 007a c400 007a c400 007a 4400  .z...z...z...zD.
    000000e0: 007a 4400 007a 4400 0000 0000 0000 0000  .zD..zD.........
    000000f0: 0000 0000 0000 0000 0000 00              ...........
</ubyte>
```

7: Now set the "numberOf" attribute in the struct_Vertex to "@vertexCount" and the name to "vertexList":

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ubyte numberOf="191">
    00000000: 0200 0000 0000 0000 0000 2041 0300 0000  .......... A....
    00000010: 0873 5f67 7261 7373 0009 7269 6e74 6572  .s_grass..rinter
    00000020: 5f66 0003 0000 0000 0000 0004 0000 0008  _f..............
    00000030: 0000 0000 0000 0001 0002 0004 0002 0003  ................
    00000040: 0000 0050 0001 00b0 0002 0000 0003 0004  ...P............
    00000050: 0001 0004 0000 0011 0000 0001 0001 0002  ................
    00000060: 0000 0005 0000 0004 0001 0050 0001 00b0  ...........P....
    00000070: 0002 0001 0004 0005 0002 0050 0002 005e  ...........P...^
    00000080: 0000 0001 0003 0001 0002 0005 0000 0404  ................
    00000090: cd00 0000 007a c400 007a c400 007a c400  .....z...z...z..
    000000a0: 007a 4400 007a 4400 007a 4400 0000 0000  .zD..zD..zD.....
    000000b0: 0000 0000 0000 0000 0000 0000 0000 00    ...............
</ubyte>
```

8: Identify the following data as an array of single floats with a counter named "heightCount":

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ubyte numberOf="179">
    00000000: 0300 0000 0873 5f67 7261 7373 0009 7269  .....s_grass..ri
    00000010: 6e74 6572 5f66 0003 0000 0000 0000 0004  nter_f..........
    00000020: 0000 0008 0000 0000 0000 0001 0002 0004  ................
    00000030: 0002 0003 0000 0050 0001 00b0 0002 0000  .......P........
    00000040: 0003 0004 0001 0004 0000 0011 0000 0001  ................
    00000050: 0001 0002 0000 0005 0000 0004 0001 0050  ...............P
    00000060: 0001 00b0 0002 0001 0004 0005 0002 0050  ...............P
    00000070: 0002 005e 0000 0001 0003 0001 0002 0005  ...^............
    00000080: 0000 0404 cd00 0000 007a c400 007a c400  .........z...z..
    00000090: 007a c400 007a 4400 007a 4400 007a 4400  .z...zD..zD..zD.
    000000a0: 0000 0000 0000 0000 0000 0000 0000 0000  ................
    000000b0: 0000 00                                  ...
</ubyte>
```

Note that the default list presentation for float is "@" and "one per row" unchecked.

9: Now we identify some strings, we see that there is a length indicator before each string and a 0x00 terminator after. Assign byte, array of byte and byte to these values respectively:

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ubyte numberOf="4">
    00000000: 0300 0000                                ....            
</ubyte>
<ubyte numberOf="1" name="length">8</ubyte>
<ubyte numberOf="@length - 1" name="characters">
    0x73, 0x5f, 0x67, 0x72, 0x61, 0x73, 0x73
</ubyte>
<ubyte numberOf="1" name="terminator">0x00</ubyte>
<ubyte numberOf="166">
    00000000: 0972 696e 7465 725f 6600 0300 0000 0000  .rinter_f.......
    00000010: 0000 0400 0000 0800 0000 0000 0000 0100  ................
    00000020: 0200 0400 0200 0300 0000 5000 0100 b000  ..........P.....
    00000030: 0200 0000 0300 0400 0100 0400 0000 1100  ................
    00000040: 0000 0100 0100 0200 0000 0500 0000 0400  ................
    00000050: 0100 5000 0100 b000 0200 0100 0400 0500  ..P.............
    00000060: 0200 5000 0200 5e00 0000 0100 0300 0100  ..P...^.........
    00000070: 0200 0500 0004 04cd 0000 0000 7ac4 0000  ............z...
    00000080: 7ac4 0000 7ac4 0000 7a44 0000 7a44 0000  z...z...zD..zD..
    00000090: 7a44 0000 0000 0000 0000 0000 0000 0000  zD..............
    000000a0: 0000 0000 0000                           ......
</ubyte>
```
Notice that you can change the presenation of integral values between binary, decimal, octal, hexadecimal and ASCII.

10: Select ASCII presentation for the bytes in the Array and notice that the default list presentation of bytes in ASCII mode is a normal string. Select the length, characters array and terminator, then assign a structure, name it "String":

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ubyte numberOf="4">
    00000000: 0300 0000                                ....            
</ubyte>
<struct_String numberOf="1">
 <ubyte numberOf="1" name="length">8</ubyte>
 <ubyte numberOf="@length - 1" name="characters">
      "s_grass"
 </ubyte>
 <ubyte numberOf="1" name="terminator">0x00</ubyte>
</struct_String>
<ubyte numberOf="166">
    00000000: 0972 696e 7465 725f 6600 0300 0000 0000  .rinter_f.......
    00000010: 0000 0400 0000 0800 0000 0000 0000 0100  ................
    00000020: 0200 0400 0200 0300 0000 5000 0100 b000  ..........P.....
    00000030: 0200 0000 0300 0400 0100 0400 0000 1100  ................
    00000040: 0000 0100 0100 0200 0000 0500 0000 0400  ................
    00000050: 0100 5000 0100 b000 0200 0100 0400 0500  ..P.............
    00000060: 0200 5000 0200 5e00 0000 0100 0300 0100  ..P...^.........
    00000070: 0200 0500 0004 04cd 0000 0000 7ac4 0000  ............z...
    00000080: 7ac4 0000 7ac4 0000 7a44 0000 7a44 0000  z...z...zD..zD..
    00000090: 7a44 0000 0000 0000 0000 0000 0000 0000  zD..............
    000000a0: 0000 0000 0000                           ......
</ubyte>
```

11: Add a presentation to the structure: "@length @characters @terminator"

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ubyte numberOf="4">
    00000000: 0300 0000                                ....            
</ubyte>
<struct_String numberOf="1">
    8 "s_grass" 0
</struct_String>
<ubyte numberOf="166">
    00000000: 0972 696e 7465 725f 6600 0300 0000 0000  .rinter_f.......
    00000010: 0000 0400 0000 0800 0000 0000 0000 0100  ................
    00000020: 0200 0400 0200 0300 0000 5000 0100 b000  ..........P.....
    00000030: 0200 0000 0300 0400 0100 0400 0000 1100  ................
    00000040: 0000 0100 0100 0200 0000 0500 0000 0400  ................
    00000050: 0100 5000 0100 b000 0200 0100 0400 0500  ..P.............
    00000060: 0200 5000 0200 5e00 0000 0100 0300 0100  ..P...^.........
    00000070: 0200 0500 0004 04cd 0000 0000 7ac4 0000  ............z...
    00000080: 7ac4 0000 7ac4 0000 7a44 0000 7a44 0000  z...z...zD..zD..
    00000090: 7a44 0000 0000 0000 0000 0000 0000 0000  zD..............
    000000a0: 0000 0000 0000                           ......
</ubyte>
```

11: Identify the counter and set the numberOf in the String. Also add a list presentation to the struct_String element, "@characters" with "one per row" checked:

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ulong name="textureCount">3</ulong>
<struct_String numberOf="@textureCount - 1" name="textureList">
    8 "s_grass" 0,
    9 "rinter_f" 0
</struct_String>
<ubyte numberOf="157">
    00000000: 0300 0000 0000 0000 0400 0000 0800 0000  ................
    00000010: 0000 0000 0100 0200 0400 0200 0300 0000  ................
    00000020: 5000 0100 b000 0200 0000 0300 0400 0100  P...............
    00000030: 0400 0000 1100 0000 0100 0100 0200 0000  ................
    00000040: 0500 0000 0400 0100 5000 0100 b000 0200  ........P.......
    00000050: 0100 0400 0500 0200 5000 0200 5e00 0000  ........P...^...
    00000060: 0100 0300 0100 0200 0500 0004 04cd 0000  ................
    00000070: 0000 7ac4 0000 7ac4 0000 7ac4 0000 7a44  ..z...z...z...zD
    00000080: 0000 7a44 0000 7a44 0000 0000 0000 0000  ..zD..zD........
    00000090: 0000 0000 0000 0000 0000 0000            ............
</ubyte>
```

12: Moving further we discover some other data structures and assign types to them:

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ulong name="textureCount">3</ulong>
<struct_String numberOf="@textureCount - 1" name="textureList">
    8 "s_grass" 0,
    9 "rinter_f" 0
</struct_String>
<ubyte numberOf="32">
    00000000: 0300 0000 0000 0000 0400 0000 0800 0000  ................
    00000010: 0000 0000 0100 0200 0400 0200 0300 0000  ................
</ubyte>
<struct_Attribute_50>
 <ushort numberOf="1" name="attributeID">0x50</ushort>
 <ushort numberOf="1" name="textureIndex">0x01</ushort>
</struct_Attribute_50>
<struct_Attribute_b0 numberOf="1">
 <ushort numberOf="1" name="attributeID">0xb0</ushort>
 <ushort numberOf="1" name="vertexCount">2</ushort>
 <ushort numberOf="@vertexCount + 2" name="vertices">
     0x00, 0x03, 0x04, 0x01
</struct_Attribute_b0>
<ubyte numberOf="108">
    00000000: 0400 0000 1100 0000 0100 0100 0200 0000  ................
    00000010: 0500 0000 0400 0100 5000 0100 b000 0200  ........P.......
    00000020: 0100 0400 0500 0200 5000 0200 5e00 0000  ........P...^...
    00000030: 0100 0300 0100 0200 0500 0004 04cd 0000  ................
    00000040: 0000 7ac4 0000 7ac4 0000 7ac4 0000 7a44  ..z...z...z...zD
    00000050: 0000 7a44 0000 7a44 0000 0000 0000 0000  ..zD..zD........
    00000060: 0000 0000 0000 0000 0000 0000            ............
</ubyte>
```
Note that the variable "vertexCount" is used in the local context. To access the vertexCount defined earlier, the syntax would have to be "@.vertexCount". The dot indicates the root element.

13: Now we realize that there is an array that can contain several different types of data structures. Rename the structure "Attribute_50" to "Attribute", right click it and select "Definition". In the definition window, enter

```xml
<struct_Attribute>
 <ushort numberOf="1" name="attributeID" />
 <switch selector="@attributeID">
  <case value="0x50">
   <ushort numberOf="1" name="textureIndex />
  </case>
  <case value="0xb0">
   <ushort numberOf="1" name="vertexCount" />
   <ushort numberOf="@vertexCount + 2" name="vertices" />
  </case>
 </switch>
</struct_Attribute>
```

TODO: This "switch" conditional operator will make presentations complicated. Perhaps a simple form of inheritance would be a better approach. In this case, the Attribute structure would be a list of ushorts, but each "subclass" of the Attribute structure assigns different meaning to the various ushorts insite, and also have a different presentation format string.

Assign this type to "Attribute_b0" as well:

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ulong name="textureCount">3</ulong>
<struct_String numberOf="@textureCount - 1" name="textureList">
    8 "s_grass" 0,
    9 "rinter_f" 0
</struct_String>
<ubyte numberOf="32">
    00000000: 0300 0000 0000 0000 0400 0000 0800 0000  ................
    00000010: 0000 0000 0100 0200 0400 0200 0300 0000  ................
</ubyte>
<struct_Attribute numberOf="1" name="attributes">
 <ushort numberOf="1" name="attributeID">0x50</ushort>
 <ushort numberOf="1" name="textureIndex">0x01</ushort>
</struct_Attribute>
<struct_Attribute numberOf="1">
 <ushort numberOf="1" name="attributeID">0xb0</ushort>
 <ushort numberOf="1" name="vertexCount">2</ushort>
 <ushort numberOf="@vertexCount + 2" name="vertices">
       0x00, 0x03, 0x04, 0x01
</struct_Attribute>
<ubyte numberOf="108">
    00000000: 0400 0000 1100 0000 0100 0100 0200 0000  ................
    00000010: 0500 0000 0400 0100 5000 0100 b000 0200  ........P.......
    00000020: 0100 0400 0500 0200 5000 0200 5e00 0000  ........P...^...
    00000030: 0100 0300 0100 0200 0500 0004 04cd 0000  ................
    00000040: 0000 7ac4 0000 7ac4 0000 7ac4 0000 7a44  ..z...z...z...zD
    00000050: 0000 7a44 0000 7a44 0000 0000 0000 0000  ..zD..zD........
    00000060: 0000 0000 0000 0000 0000 0000            ............
</ubyte>
```
Notice how the same structure looks different due to the conditionals.

14: Identify and tag the counter that controls this array, the counter defines the total number of shorts in the array, regardless of the various attribute structures. We will use a different expression for the numberOf field of the array to get this working. If the this expression results in an integral value, this value is used as the number of elements, but if the expression evaluates to a boolean true or false the result is used differently. When deciding if the next data block is part of the array, the expression is evaluated, if it returns true the data block is considered part of the array and the next data block is tested. We will also use another new feature, the system provides some system variables, for example the size in bytes of the current data structure can be referenced by the variable named "@_bytesize":

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ulong name="textureCount">3</ulong>
<struct_String numberOf="@textureCount - 1" name="textureList">
    8 "s_grass" 0,
    9 "rinter_f" 0
</struct_String>
<ubyte numberOf="12">
    00000000: 0300 0000 0000 0000 0400 0000            ............
</ubyte>
<ulong numberOf="1" name="attributeShortCount">8</ulong>
<ubyte numberOf="16">
    00000000: 0000 0000 0100 0200 0400 0200 0300 0000  ................
</ubyte>
<struct_Attribute numberOf="@_bytesize * 2 < @attributeShortCount" name="attributes">
 {
   attributeID = 0x50
   textureIndex = 0x01
 },
 {
   attributeID = 0xb0
   vertexCount = 2
   vertices = 0x00, 0x03, 0x04, 0x01
 }
</struct_Attribute>
<ubyte numberOf="108">
    00000000: 0400 0000 1100 0000 0100 0100 0200 0000  ................
    00000010: 0500 0000 0400 0100 5000 0100 b000 0200  ........P.......
    00000020: 0100 0400 0500 0200 5000 0200 5e00 0000  ........P...^...
    00000030: 0100 0300 0100 0200 0500 0004 04cd 0000  ................
    00000040: 0000 7ac4 0000 7ac4 0000 7ac4 0000 7a44  ..z...z...z...zD
    00000050: 0000 7a44 0000 7a44 0000 0000 0000 0000  ..zD..zD........
    00000060: 0000 0000 0000 0000 0000 0000            ............
</ubyte>
```
Notice how the default list presentation for a structure looks. Remember that we defined our own list presentation in the case of the vertexList element.



15: Identify and assign types to some other values:

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ulong name="textureCount">3</ulong>
<struct_String numberOf="@textureCount - 1" name="textureList">
    8 "s_grass" 0,
    9 "rinter_f" 0
</struct_String>
<ulong numberOf="1" name="blockCount">0x03</ulong>
<ubyte numberOf="4">
    00000000: 0000 0000                               ....
</ubyte>
<struct_Block numberOf="1" name="blocklist">
 <ulong name="perimeterVertexCount">0x04</ulong>
 <ulong name="attributeShortCount">8</ulong>
 <struct_PerimeterVertex numberOf="@perimeterVertexCount" name="perimeter">
   {vertexRef = 0x00, blockRef = 0x00},
   {vertexRef = 0x01, blockRef = 0x02},
   {vertexRef = 0x04, blockRef = 0x02},
   {vertexRef = 0x03, blockRef = 0x00}
 </struct_PerimeterVertex>
 <struct_Attribute numberOf="@_bytesize * 2 < @attributeShortCount" name="attributes">
  {
    attributeID = 0x50
    textureIndex = 0x01
  },
  {
    attributeID = 0xb0
    vertexCount = 2
    vertices = 0x00, 0x03, 0x04, 0x01
  }
 </struct_Attribute>
</struct_Block>
<ubyte numberOf="108">
    00000000: 0400 0000 1100 0000 0100 0100 0200 0000  ................
    00000010: 0500 0000 0400 0100 5000 0100 b000 0200  ........P.......
    00000020: 0100 0400 0500 0200 5000 0200 5e00 0000  ........P...^...
    00000030: 0100 0300 0100 0200 0500 0004 04cd 0000  ................
    00000040: 0000 7ac4 0000 7ac4 0000 7ac4 0000 7a44  ..z...z...z...zD
    00000050: 0000 7a44 0000 7a44 0000 0000 0000 0000  ..zD..zD........
    00000060: 0000 0000 0000 0000 0000 0000            ............
</ubyte>
```
Note that the list presentation for a PerimeterVertex is "{vertexRef = @vertexRef, blockRef = @blockRef}" with "one per row" checked.


16: Now set the numberOf in the Block, then you will get an error message - an attributeID that has no case element is found, thus the array does not match! The array will be ended at the last complete element and the data that caused the error, in this case the unknown attributeID will be highlighted:

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ulong name="textureCount">3</ulong>
<struct_String numberOf="@textureCount - 1" name="textureList">
    8 "s_grass" 0,
    9 "rinter_f" 0
</struct_String>
<ulong numberOf="1" name="blockCount">0x03</ulong>
<ubyte numberOf="4">
    00000000: 0000 0000                               ....
</ubyte>
<struct_Block numberOf="@blockCount - 1" name="blockList">
 <ulong name="perimeterVertexCount">0x04</ulong>
 <ulong name="attributeShortCount">8</ulong>
 <struct_PerimeterVertex numberOf="@perimeterVertexCount" name="perimeter">
   {vertexRef = 0x00, blockRef = 0x00},
   {vertexRef = 0x01, blockRef = 0x02},
   {vertexRef = 0x04, blockRef = 0x02},
   {vertexRef = 0x03, blockRef = 0x00}
 </struct_PerimeterVertex>
 <struct_Attribute numberOf="@_bytesize * 2 < @attributeShortCount" name="attributes">
  {
    attributeID = 0x50
    textureIndex = 0x01
  },
  {
    attributeID = 0xb0
    vertexCount = 2
    vertices = 0x00, 0x03, 0x04, 0x01
  }
 </struct_Attribute>
</struct_Block>
<ubyte numberOf="108">
    00000000: 0400 0000 1100 0000 0100 0100 0200 0000  ................
    00000010: 0500 0000 0400 0100 5000 0100 b000 0200  ........P.......
    00000020: 0100 0400 0500 0200 5000 0200[5e00]0000  ........P...^...
    00000030: 0100 0300 0100 0200 0500 0004 04cd 0000  ................
    00000040: 0000 7ac4 0000 7ac4 0000 7ac4 0000 7a44  ..z...z...z...zD
    00000050: 0000 7a44 0000 7a44 0000 0000 0000 0000  ..zD..zD........
    00000060: 0000 0000 0000 0000 0000 0000            ............
</ubyte>
```

17: Add the new attribute type in the definition of the Attribute type:

```xml
<struct_Attribute>
 <ushort numberOf="1" name="attributeID" />
 <switch selector="@attributeID">
  <case value="0x50">
   <ushort numberOf="1" name="textureIndex />
  </case>
  <case value="0xb0">
   <ushort numberOf="1" name="vertexCount" />
   <ushort numberOf="@vertexCount + 2" name="vertices" />
  </case>
  <case value="0x5e">
   <ushort numberOf="1" name="bottomHeight"/>
   <ushort numberOf="1" name="topHeight"/>
   <ushort numberOf="1" name="repeatX"/>
   <ushort numberOf="1" name="repeatY"/>
   <ushort numberOf="1" name="leftVertex"/>
   <ushort numberOf="1" name="rightVertex"/>
  </case>
 </switch>
</struct_Attribute>
```

The error disappears and the second "Block" becomes part of the array:

```xml
<ubyte numberOf="8">
    00000000: 5053 4430 0200 0000                      PSD0....        
</ubyte>
<ulong numberOf="1" name="vertexCount">6</ulong>
<struct_Vertex numberOf="@vertexCount" name="vertexList">
    (0.123, 1.22, 0.2),
    (2.0, 1.22, 0.23),
    (4.5, 1.22, 0.2),
    (0.123, 1.22, 2.0),
    (2.0, 1.22, 2.0),
    (4.0, 0.0, 2.0)
</struct_Vertex>
<ulong numberOf="1" name="heightCount">2</ulong>
<float numberOf="@heightCount" name="heightList">
    0.0, 10.0
</float>
<ulong name="textureCount">3</ulong>
<struct_String numberOf="@textureCount - 1" name="textureList">
    8 "s_grass" 0,
    9 "rinter_f" 0
</struct_String>
<ulong numberOf="1" name="blockCount">0x03</ulong>
<ubyte numberOf="4">
    00000000: 0000 0000                               ....
</ubyte>
<struct_Block numberOf="@blockCount - 1" name="blockList">
 {
  perimeterVertexCount = 0x04
  attributeShortCount = 0x08
  perimeter = {vertexRef = 0x00, blockRef = 0x00},
              {vertexRef = 0x01, blockRef = 0x02},
              {vertexRef = 0x04, blockRef = 0x02},
              {vertexRef = 0x03, blockRef = 0x00}
  attributes = 
   {
    attributeID = 0x50
    textureIndex = 0x01
   },
   {
    attributeID = 0xb0
    vertexCount = 2
    vertices = 0x00, 0x03, 0x04, 0x01
   }
 },
 {
  perimeterVertexCount = 0x04
  attributeShortCount = 0x11
  perimeter = {vertexRef = 0x01, blockRef = 0x01},
              {vertexRef = 0x02, blockRef = 0x02},
              {vertexRef = 0x05, blockRef = 0x02},
              {vertexRef = 0x04, blockRef = 0x01}
  attributes =
   {
    attributeID = 0x50
    textureIndex = 0x02
   },
   {
    attributeID = 0x5e
    bottomHeight = 0x00
    topHeight = 0x01
    repeatX = 0x03
    repeatY = 0x01
    leftVertex = 0x02
    rightVertex = 0x05
   }
 }
</struct_Block>
<ubyte numberOf="50">
    00000000: 0004 04cd 0000 0000 7ac4 0000 7ac4 0000  ........z...z...
    00000010: 7ac4 0000 7a44 0000 7a44 0000 7a44 0000  z...zD..zD..zD..
    00000020: 0000 0000 0000 0000 0000 0000 0000 0000  ................
    00000030: 0000                                     ..
</ubyte>
```

18: Keep this up until the entire file is mapped, export a schema and the generic HEXML reader can read file that conforms to this specification, binary or in the XML format. Remember that even though we have selected various presentations for our datatypes the base format is always the xml specified at the finest level.





The biggest difficulty with this approach is the expressions. As they are defined here they represent unidirectional constraints. This works fine if we are only interested in reading existsing files. If we want to edit the file and write it back, these constraints must be bidirectional. At the moment I have no clear idea about how this can be achieved, but if this is solved we not only get an editor that helps us map file formats, we also get a generic editor capable of editing just about any file.

Absolute path to the variables defined in this example:

```
.vertexCount
.vertexList[n].x
.vertexList[n].y
.vertexList[n].z
.heightCount
.heightList[n]
.textureCount
.textureList[n].length
.textureList[n].characters
.textureList[n].terminator
.blockCount
.blockList[n].perimeterVertexCount
.blockList[n].attributeShortCount
.blockList[n].perimeter[m].vertexRef
.blockList[n].perimeter[m].blockRef
.blockList[n].attributes[m].attributeID
.blockList[n].attributes[m].textureIndex
.blockList[n].attributes[m].vertexCount
.blockList[n].attributes[m].vertices[l]
.blockList[n].attributes[m].bottomHeight
.blockList[n].attributes[m].topHeight
.blockList[n].attributes[m].repeatX
.blockList[n].attributes[m].repeatY
.blockList[n].attributes[m].leftVertex
.blockList[n].attributes[m].rightVertex
```
