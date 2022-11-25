using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace Internal;

//https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa/24343727#24343727
internal static class Hex
{
    public const int Min = 48;
    public const int Max = 102;

    public static class Lower16
    {
        internal static readonly ushort[] _map = new ushort[] {
        12336, //0
        12592, //1
        12848, //2
        13104, //3
        13360, //4
        13616, //5
        13872, //6
        14128, //7
        14384, //8
        14640, //9
        24880, //10
        25136, //11
        25392, //12
        25648, //13
        25904, //14
        26160, //15
        12337, //16
        12593, //17
        12849, //18
        13105, //19
        13361, //20
        13617, //21
        13873, //22
        14129, //23
        14385, //24
        14641, //25
        24881, //26
        25137, //27
        25393, //28
        25649, //29
        25905, //30
        26161, //31
        12338, //32
        12594, //33
        12850, //34
        13106, //35
        13362, //36
        13618, //37
        13874, //38
        14130, //39
        14386, //40
        14642, //41
        24882, //42
        25138, //43
        25394, //44
        25650, //45
        25906, //46
        26162, //47
        12339, //48
        12595, //49
        12851, //50
        13107, //51
        13363, //52
        13619, //53
        13875, //54
        14131, //55
        14387, //56
        14643, //57
        24883, //58
        25139, //59
        25395, //60
        25651, //61
        25907, //62
        26163, //63
        12340, //64
        12596, //65
        12852, //66
        13108, //67
        13364, //68
        13620, //69
        13876, //70
        14132, //71
        14388, //72
        14644, //73
        24884, //74
        25140, //75
        25396, //76
        25652, //77
        25908, //78
        26164, //79
        12341, //80
        12597, //81
        12853, //82
        13109, //83
        13365, //84
        13621, //85
        13877, //86
        14133, //87
        14389, //88
        14645, //89
        24885, //90
        25141, //91
        25397, //92
        25653, //93
        25909, //94
        26165, //95
        12342, //96
        12598, //97
        12854, //98
        13110, //99
        13366, //100
        13622, //101
        13878, //102
        14134, //103
        14390, //104
        14646, //105
        24886, //106
        25142, //107
        25398, //108
        25654, //109
        25910, //110
        26166, //111
        12343, //112
        12599, //113
        12855, //114
        13111, //115
        13367, //116
        13623, //117
        13879, //118
        14135, //119
        14391, //120
        14647, //121
        24887, //122
        25143, //123
        25399, //124
        25655, //125
        25911, //126
        26167, //127
        12344, //128
        12600, //129
        12856, //130
        13112, //131
        13368, //132
        13624, //133
        13880, //134
        14136, //135
        14392, //136
        14648, //137
        24888, //138
        25144, //139
        25400, //140
        25656, //141
        25912, //142
        26168, //143
        12345, //144
        12601, //145
        12857, //146
        13113, //147
        13369, //148
        13625, //149
        13881, //150
        14137, //151
        14393, //152
        14649, //153
        24889, //154
        25145, //155
        25401, //156
        25657, //157
        25913, //158
        26169, //159
        12385, //160
        12641, //161
        12897, //162
        13153, //163
        13409, //164
        13665, //165
        13921, //166
        14177, //167
        14433, //168
        14689, //169
        24929, //170
        25185, //171
        25441, //172
        25697, //173
        25953, //174
        26209, //175
        12386, //176
        12642, //177
        12898, //178
        13154, //179
        13410, //180
        13666, //181
        13922, //182
        14178, //183
        14434, //184
        14690, //185
        24930, //186
        25186, //187
        25442, //188
        25698, //189
        25954, //190
        26210, //191
        12387, //192
        12643, //193
        12899, //194
        13155, //195
        13411, //196
        13667, //197
        13923, //198
        14179, //199
        14435, //200
        14691, //201
        24931, //202
        25187, //203
        25443, //204
        25699, //205
        25955, //206
        26211, //207
        12388, //208
        12644, //209
        12900, //210
        13156, //211
        13412, //212
        13668, //213
        13924, //214
        14180, //215
        14436, //216
        14692, //217
        24932, //218
        25188, //219
        25444, //220
        25700, //221
        25956, //222
        26212, //223
        12389, //224
        12645, //225
        12901, //226
        13157, //227
        13413, //228
        13669, //229
        13925, //230
        14181, //231
        14437, //232
        14693, //233
        24933, //234
        25189, //235
        25445, //236
        25701, //237
        25957, //238
        26213, //239
        12390, //240
        12646, //241
        12902, //242
        13158, //243
        13414, //244
        13670, //245
        13926, //246
        14182, //247
        14438, //248
        14694, //249
        24934, //250
        25190, //251
        25446, //252
        25702, //253
        25958, //254
        26214, //255
        };

        public static readonly unsafe ushort* Map = (ushort*)GCHandle.Alloc(_map, GCHandleType.Pinned).AddrOfPinnedObject();
    }

    public static class Lower32
    {
        internal static readonly uint[] _map = new uint[] {
        3145776, //0
        3211312, //1
        3276848, //2
        3342384, //3
        3407920, //4
        3473456, //5
        3538992, //6
        3604528, //7
        3670064, //8
        3735600, //9
        6357040, //10
        6422576, //11
        6488112, //12
        6553648, //13
        6619184, //14
        6684720, //15
        3145777, //16
        3211313, //17
        3276849, //18
        3342385, //19
        3407921, //20
        3473457, //21
        3538993, //22
        3604529, //23
        3670065, //24
        3735601, //25
        6357041, //26
        6422577, //27
        6488113, //28
        6553649, //29
        6619185, //30
        6684721, //31
        3145778, //32
        3211314, //33
        3276850, //34
        3342386, //35
        3407922, //36
        3473458, //37
        3538994, //38
        3604530, //39
        3670066, //40
        3735602, //41
        6357042, //42
        6422578, //43
        6488114, //44
        6553650, //45
        6619186, //46
        6684722, //47
        3145779, //48
        3211315, //49
        3276851, //50
        3342387, //51
        3407923, //52
        3473459, //53
        3538995, //54
        3604531, //55
        3670067, //56
        3735603, //57
        6357043, //58
        6422579, //59
        6488115, //60
        6553651, //61
        6619187, //62
        6684723, //63
        3145780, //64
        3211316, //65
        3276852, //66
        3342388, //67
        3407924, //68
        3473460, //69
        3538996, //70
        3604532, //71
        3670068, //72
        3735604, //73
        6357044, //74
        6422580, //75
        6488116, //76
        6553652, //77
        6619188, //78
        6684724, //79
        3145781, //80
        3211317, //81
        3276853, //82
        3342389, //83
        3407925, //84
        3473461, //85
        3538997, //86
        3604533, //87
        3670069, //88
        3735605, //89
        6357045, //90
        6422581, //91
        6488117, //92
        6553653, //93
        6619189, //94
        6684725, //95
        3145782, //96
        3211318, //97
        3276854, //98
        3342390, //99
        3407926, //100
        3473462, //101
        3538998, //102
        3604534, //103
        3670070, //104
        3735606, //105
        6357046, //106
        6422582, //107
        6488118, //108
        6553654, //109
        6619190, //110
        6684726, //111
        3145783, //112
        3211319, //113
        3276855, //114
        3342391, //115
        3407927, //116
        3473463, //117
        3538999, //118
        3604535, //119
        3670071, //120
        3735607, //121
        6357047, //122
        6422583, //123
        6488119, //124
        6553655, //125
        6619191, //126
        6684727, //127
        3145784, //128
        3211320, //129
        3276856, //130
        3342392, //131
        3407928, //132
        3473464, //133
        3539000, //134
        3604536, //135
        3670072, //136
        3735608, //137
        6357048, //138
        6422584, //139
        6488120, //140
        6553656, //141
        6619192, //142
        6684728, //143
        3145785, //144
        3211321, //145
        3276857, //146
        3342393, //147
        3407929, //148
        3473465, //149
        3539001, //150
        3604537, //151
        3670073, //152
        3735609, //153
        6357049, //154
        6422585, //155
        6488121, //156
        6553657, //157
        6619193, //158
        6684729, //159
        3145825, //160
        3211361, //161
        3276897, //162
        3342433, //163
        3407969, //164
        3473505, //165
        3539041, //166
        3604577, //167
        3670113, //168
        3735649, //169
        6357089, //170
        6422625, //171
        6488161, //172
        6553697, //173
        6619233, //174
        6684769, //175
        3145826, //176
        3211362, //177
        3276898, //178
        3342434, //179
        3407970, //180
        3473506, //181
        3539042, //182
        3604578, //183
        3670114, //184
        3735650, //185
        6357090, //186
        6422626, //187
        6488162, //188
        6553698, //189
        6619234, //190
        6684770, //191
        3145827, //192
        3211363, //193
        3276899, //194
        3342435, //195
        3407971, //196
        3473507, //197
        3539043, //198
        3604579, //199
        3670115, //200
        3735651, //201
        6357091, //202
        6422627, //203
        6488163, //204
        6553699, //205
        6619235, //206
        6684771, //207
        3145828, //208
        3211364, //209
        3276900, //210
        3342436, //211
        3407972, //212
        3473508, //213
        3539044, //214
        3604580, //215
        3670116, //216
        3735652, //217
        6357092, //218
        6422628, //219
        6488164, //220
        6553700, //221
        6619236, //222
        6684772, //223
        3145829, //224
        3211365, //225
        3276901, //226
        3342437, //227
        3407973, //228
        3473509, //229
        3539045, //230
        3604581, //231
        3670117, //232
        3735653, //233
        6357093, //234
        6422629, //235
        6488165, //236
        6553701, //237
        6619237, //238
        6684773, //239
        3145830, //240
        3211366, //241
        3276902, //242
        3342438, //243
        3407974, //244
        3473510, //245
        3539046, //246
        3604582, //247
        3670118, //248
        3735654, //249
        6357094, //250
        6422630, //251
        6488166, //252
        6553702, //253
        6619238, //254
        6684774, //255
        };

        public static readonly unsafe uint* Map = (uint*)GCHandle.Alloc(_map, GCHandleType.Pinned).AddrOfPinnedObject();
    }

    public static class Upper16
    {
        internal static readonly ushort[] _map = new ushort[] {
        12336, //0
        12592, //1
        12848, //2
        13104, //3
        13360, //4
        13616, //5
        13872, //6
        14128, //7
        14384, //8
        14640, //9
        16688, //10
        16944, //11
        17200, //12
        17456, //13
        17712, //14
        17968, //15
        12337, //16
        12593, //17
        12849, //18
        13105, //19
        13361, //20
        13617, //21
        13873, //22
        14129, //23
        14385, //24
        14641, //25
        16689, //26
        16945, //27
        17201, //28
        17457, //29
        17713, //30
        17969, //31
        12338, //32
        12594, //33
        12850, //34
        13106, //35
        13362, //36
        13618, //37
        13874, //38
        14130, //39
        14386, //40
        14642, //41
        16690, //42
        16946, //43
        17202, //44
        17458, //45
        17714, //46
        17970, //47
        12339, //48
        12595, //49
        12851, //50
        13107, //51
        13363, //52
        13619, //53
        13875, //54
        14131, //55
        14387, //56
        14643, //57
        16691, //58
        16947, //59
        17203, //60
        17459, //61
        17715, //62
        17971, //63
        12340, //64
        12596, //65
        12852, //66
        13108, //67
        13364, //68
        13620, //69
        13876, //70
        14132, //71
        14388, //72
        14644, //73
        16692, //74
        16948, //75
        17204, //76
        17460, //77
        17716, //78
        17972, //79
        12341, //80
        12597, //81
        12853, //82
        13109, //83
        13365, //84
        13621, //85
        13877, //86
        14133, //87
        14389, //88
        14645, //89
        16693, //90
        16949, //91
        17205, //92
        17461, //93
        17717, //94
        17973, //95
        12342, //96
        12598, //97
        12854, //98
        13110, //99
        13366, //100
        13622, //101
        13878, //102
        14134, //103
        14390, //104
        14646, //105
        16694, //106
        16950, //107
        17206, //108
        17462, //109
        17718, //110
        17974, //111
        12343, //112
        12599, //113
        12855, //114
        13111, //115
        13367, //116
        13623, //117
        13879, //118
        14135, //119
        14391, //120
        14647, //121
        16695, //122
        16951, //123
        17207, //124
        17463, //125
        17719, //126
        17975, //127
        12344, //128
        12600, //129
        12856, //130
        13112, //131
        13368, //132
        13624, //133
        13880, //134
        14136, //135
        14392, //136
        14648, //137
        16696, //138
        16952, //139
        17208, //140
        17464, //141
        17720, //142
        17976, //143
        12345, //144
        12601, //145
        12857, //146
        13113, //147
        13369, //148
        13625, //149
        13881, //150
        14137, //151
        14393, //152
        14649, //153
        16697, //154
        16953, //155
        17209, //156
        17465, //157
        17721, //158
        17977, //159
        12353, //160
        12609, //161
        12865, //162
        13121, //163
        13377, //164
        13633, //165
        13889, //166
        14145, //167
        14401, //168
        14657, //169
        16705, //170
        16961, //171
        17217, //172
        17473, //173
        17729, //174
        17985, //175
        12354, //176
        12610, //177
        12866, //178
        13122, //179
        13378, //180
        13634, //181
        13890, //182
        14146, //183
        14402, //184
        14658, //185
        16706, //186
        16962, //187
        17218, //188
        17474, //189
        17730, //190
        17986, //191
        12355, //192
        12611, //193
        12867, //194
        13123, //195
        13379, //196
        13635, //197
        13891, //198
        14147, //199
        14403, //200
        14659, //201
        16707, //202
        16963, //203
        17219, //204
        17475, //205
        17731, //206
        17987, //207
        12356, //208
        12612, //209
        12868, //210
        13124, //211
        13380, //212
        13636, //213
        13892, //214
        14148, //215
        14404, //216
        14660, //217
        16708, //218
        16964, //219
        17220, //220
        17476, //221
        17732, //222
        17988, //223
        12357, //224
        12613, //225
        12869, //226
        13125, //227
        13381, //228
        13637, //229
        13893, //230
        14149, //231
        14405, //232
        14661, //233
        16709, //234
        16965, //235
        17221, //236
        17477, //237
        17733, //238
        17989, //239
        12358, //240
        12614, //241
        12870, //242
        13126, //243
        13382, //244
        13638, //245
        13894, //246
        14150, //247
        14406, //248
        14662, //249
        16710, //250
        16966, //251
        17222, //252
        17478, //253
        17734, //254
        17990, //255
        };

        public static readonly unsafe ushort* Map = (ushort*)GCHandle.Alloc(_map, GCHandleType.Pinned).AddrOfPinnedObject();
    }

    public static class Upper32
    {
        internal static readonly uint[] _map = new uint[] {
        3145776, //0
        3211312, //1
        3276848, //2
        3342384, //3
        3407920, //4
        3473456, //5
        3538992, //6
        3604528, //7
        3670064, //8
        3735600, //9
        4259888, //10
        4325424, //11
        4390960, //12
        4456496, //13
        4522032, //14
        4587568, //15
        3145777, //16
        3211313, //17
        3276849, //18
        3342385, //19
        3407921, //20
        3473457, //21
        3538993, //22
        3604529, //23
        3670065, //24
        3735601, //25
        4259889, //26
        4325425, //27
        4390961, //28
        4456497, //29
        4522033, //30
        4587569, //31
        3145778, //32
        3211314, //33
        3276850, //34
        3342386, //35
        3407922, //36
        3473458, //37
        3538994, //38
        3604530, //39
        3670066, //40
        3735602, //41
        4259890, //42
        4325426, //43
        4390962, //44
        4456498, //45
        4522034, //46
        4587570, //47
        3145779, //48
        3211315, //49
        3276851, //50
        3342387, //51
        3407923, //52
        3473459, //53
        3538995, //54
        3604531, //55
        3670067, //56
        3735603, //57
        4259891, //58
        4325427, //59
        4390963, //60
        4456499, //61
        4522035, //62
        4587571, //63
        3145780, //64
        3211316, //65
        3276852, //66
        3342388, //67
        3407924, //68
        3473460, //69
        3538996, //70
        3604532, //71
        3670068, //72
        3735604, //73
        4259892, //74
        4325428, //75
        4390964, //76
        4456500, //77
        4522036, //78
        4587572, //79
        3145781, //80
        3211317, //81
        3276853, //82
        3342389, //83
        3407925, //84
        3473461, //85
        3538997, //86
        3604533, //87
        3670069, //88
        3735605, //89
        4259893, //90
        4325429, //91
        4390965, //92
        4456501, //93
        4522037, //94
        4587573, //95
        3145782, //96
        3211318, //97
        3276854, //98
        3342390, //99
        3407926, //100
        3473462, //101
        3538998, //102
        3604534, //103
        3670070, //104
        3735606, //105
        4259894, //106
        4325430, //107
        4390966, //108
        4456502, //109
        4522038, //110
        4587574, //111
        3145783, //112
        3211319, //113
        3276855, //114
        3342391, //115
        3407927, //116
        3473463, //117
        3538999, //118
        3604535, //119
        3670071, //120
        3735607, //121
        4259895, //122
        4325431, //123
        4390967, //124
        4456503, //125
        4522039, //126
        4587575, //127
        3145784, //128
        3211320, //129
        3276856, //130
        3342392, //131
        3407928, //132
        3473464, //133
        3539000, //134
        3604536, //135
        3670072, //136
        3735608, //137
        4259896, //138
        4325432, //139
        4390968, //140
        4456504, //141
        4522040, //142
        4587576, //143
        3145785, //144
        3211321, //145
        3276857, //146
        3342393, //147
        3407929, //148
        3473465, //149
        3539001, //150
        3604537, //151
        3670073, //152
        3735609, //153
        4259897, //154
        4325433, //155
        4390969, //156
        4456505, //157
        4522041, //158
        4587577, //159
        3145793, //160
        3211329, //161
        3276865, //162
        3342401, //163
        3407937, //164
        3473473, //165
        3539009, //166
        3604545, //167
        3670081, //168
        3735617, //169
        4259905, //170
        4325441, //171
        4390977, //172
        4456513, //173
        4522049, //174
        4587585, //175
        3145794, //176
        3211330, //177
        3276866, //178
        3342402, //179
        3407938, //180
        3473474, //181
        3539010, //182
        3604546, //183
        3670082, //184
        3735618, //185
        4259906, //186
        4325442, //187
        4390978, //188
        4456514, //189
        4522050, //190
        4587586, //191
        3145795, //192
        3211331, //193
        3276867, //194
        3342403, //195
        3407939, //196
        3473475, //197
        3539011, //198
        3604547, //199
        3670083, //200
        3735619, //201
        4259907, //202
        4325443, //203
        4390979, //204
        4456515, //205
        4522051, //206
        4587587, //207
        3145796, //208
        3211332, //209
        3276868, //210
        3342404, //211
        3407940, //212
        3473476, //213
        3539012, //214
        3604548, //215
        3670084, //216
        3735620, //217
        4259908, //218
        4325444, //219
        4390980, //220
        4456516, //221
        4522052, //222
        4587588, //223
        3145797, //224
        3211333, //225
        3276869, //226
        3342405, //227
        3407941, //228
        3473477, //229
        3539013, //230
        3604549, //231
        3670085, //232
        3735621, //233
        4259909, //234
        4325445, //235
        4390981, //236
        4456517, //237
        4522053, //238
        4587589, //239
        3145798, //240
        3211334, //241
        3276870, //242
        3342406, //243
        3407942, //244
        3473478, //245
        3539014, //246
        3604550, //247
        3670086, //248
        3735622, //249
        4259910, //250
        4325446, //251
        4390982, //252
        4456518, //253
        4522054, //254
        4587590, //255
        };

        public static readonly unsafe uint* Map = (uint*)GCHandle.Alloc(_map, GCHandleType.Pinned).AddrOfPinnedObject();
    }

    public static readonly sbyte[] DecodeMap = new sbyte[] {
    -1, //0
    -1, //1
    -1, //2
    -1, //3
    -1, //4
    -1, //5
    -1, //6
    -1, //7
    -1, //8
    -1, //9
    -1, //10
    -1, //11
    -1, //12
    -1, //13
    -1, //14
    -1, //15
    -1, //16
    -1, //17
    -1, //18
    -1, //19
    -1, //20
    -1, //21
    -1, //22
    -1, //23
    -1, //24
    -1, //25
    -1, //26
    -1, //27
    -1, //28
    -1, //29
    -1, //30
    -1, //31
    -1, //32
    -1, //33
    -1, //34
    -1, //35
    -1, //36
    -1, //37
    -1, //38
    -1, //39
    -1, //40
    -1, //41
    -1, //42
    -1, //43
    -1, //44
    -1, //45
    -1, //46
    -1, //47
     0, //48 -> 0
     1, //49 -> 1
     2, //50 -> 2
     3, //51 -> 3
     4, //52 -> 4
     5, //53 -> 5
     6, //54 -> 6
     7, //55 -> 7
     8, //56 -> 8
     9, //57 -> 9
    -1, //58
    -1, //59
    -1, //60
    -1, //61
    -1, //62
    -1, //63
    -1, //64
    10, //65 -> A
    11, //66 -> B
    12, //67 -> C
    13, //68 -> D
    14, //69 -> E
    15, //70 -> F
    -1, //71
    -1, //72
    -1, //73
    -1, //74
    -1, //75
    -1, //76
    -1, //77
    -1, //78
    -1, //79
    -1, //80
    -1, //81
    -1, //82
    -1, //83
    -1, //84
    -1, //85
    -1, //86
    -1, //87
    -1, //88
    -1, //89
    -1, //90
    -1, //91
    -1, //92
    -1, //93
    -1, //94
    -1, //95
    -1, //96
    10, //97 -> a
    11, //98 -> b
    12, //99 -> c
    13, //100 -> d
    14, //101 -> e
    15, //102 -> f
    };

    #region Number

    internal static readonly Char[] _numLower = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
    internal static readonly Char[] _numUpper = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

    public const UInt32 NumCount1 = 16;
    public const UInt32 NumCount2 = NumCount1 * NumCount1;
    public const UInt32 NumSum2 = NumCount2 + NumCount1;
    public const UInt32 NumCount3 = NumCount2 * NumCount1;
    public const UInt32 NumSum3 = NumCount3 + NumSum2;
    public const UInt32 NumCount4 = NumCount3 * NumCount1;
    public const UInt32 NumSum4 = NumCount4 + NumSum3;
    public const UInt32 NumCount5 = NumCount4 * NumCount1;
    public const UInt32 NumSum5 = NumCount5 + NumSum4;
    public const UInt32 NumCount6 = NumCount5 * NumCount1;
    public const UInt32 NumSum6 = NumCount6 + NumSum5;
    public const UInt32 NumCount7 = NumCount6 * NumCount1;
    public const UInt32 NumSum7 = NumCount7 + NumSum6;
    public const UInt64 NumCount8 = (UInt64)NumCount7 * NumCount1;
    //public const UInt64 NumSum8 = NumCount8 + NumSum7;

    public static String ToString(UInt32 value)
    {
        var len = GetLength(value);

        var str = new string('\0', len);

        unsafe
        {
            fixed (char* ptr = str)
            {
                var chars = new Span<Char>(ptr, len);

                TryWrite(chars, value, _numLower);
            }
        }
        return str;
    }

    public static OperationStatus TryWrite(Span<Char> chars, UInt32 value, Char[] map)
    {
        if (chars.Length == 0) return OperationStatus.DestinationTooSmall;

        if (value < NumCount1)
        {
            chars[0] = map[value];

            return OperationStatus.Done;
        }

        if (value < NumSum2)
        {
            if (chars.Length < 2) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumCount1) >> 4];
            chars[1] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum3)
        {
            if (chars.Length < 3) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumSum2) >> 8];
            chars[1] = map[(value &= NumCount2 - 1) >> 4];
            chars[2] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum4)
        {
            if (chars.Length < 4) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumSum3) >> 12];
            chars[1] = map[(value &= NumCount3 - 1) >> 8];
            chars[2] = map[(value &= NumCount2 - 1) >> 4];
            chars[3] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum5)
        {
            if (chars.Length < 5) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumSum4) >> 16];
            chars[1] = map[(value &= NumCount4 - 1) >> 12];
            chars[2] = map[(value &= NumCount3 - 1) >> 8];
            chars[3] = map[(value &= NumCount2 - 1) >> 4];
            chars[4] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum6)
        {
            if (chars.Length < 6) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumSum5) >> 20];
            chars[1] = map[(value &= NumCount5 - 1) >> 16];
            chars[2] = map[(value &= NumCount4 - 1) >> 12];
            chars[3] = map[(value &= NumCount3 - 1) >> 8];
            chars[4] = map[(value &= NumCount2 - 1) >> 4];
            chars[5] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum7)
        {
            if (chars.Length < 7) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumSum6) >> 24];
            chars[1] = map[(value &= NumCount6 - 1) >> 20];
            chars[2] = map[(value &= NumCount5 - 1) >> 16];
            chars[3] = map[(value &= NumCount4 - 1) >> 12];
            chars[4] = map[(value &= NumCount3 - 1) >> 8];
            chars[5] = map[(value &= NumCount2 - 1) >> 4];
            chars[6] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (chars.Length < 8) return OperationStatus.DestinationTooSmall;

        chars[0] = map[(value -= NumSum7) >> 28];
        chars[1] = map[(value &= NumCount7 - 1) >> 24];
        chars[2] = map[(value &= NumCount6 - 1) >> 20];
        chars[3] = map[(value &= NumCount5 - 1) >> 16];
        chars[4] = map[(value &= NumCount4 - 1) >> 12];
        chars[5] = map[(value &= NumCount3 - 1) >> 8];
        chars[6] = map[(value &= NumCount2 - 1) >> 4];
        chars[7] = map[value & NumCount1 - 1];

        return OperationStatus.Done;
    }

    public static Byte ToByte(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;

        if (len == 0) throw new ArgumentException(nameof(chars));

        if (len > 2) throw new ArgumentOutOfRangeException(nameof(chars), "Length 1-2");

        if (len == 1) return GetIndexNumByte(chars[0]);

        return checked((byte)((GetIndexNumByte(chars[0]) << 4) + GetIndexNumByte(chars[1]) + NumCount1));
    }

    public static UInt32 ToUInt32(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;

        if (len == 0) throw new ArgumentException(nameof(chars));

        if (len > 8) throw new ArgumentOutOfRangeException(nameof(chars), "Length 1-6");

        if (len == 1) return GetIndexNum(chars[0]);

        if (len == 2) return (GetIndexNum(chars[0]) << 4) + GetIndexNum(chars[1]) + NumCount1;

        if (len == 3) return (GetIndexNum(chars[0]) << 8) + (GetIndexNum(chars[1]) << 4) + GetIndexNum(chars[2]) + NumSum2;

        if (len == 4) return (GetIndexNum(chars[0]) << 12) + (GetIndexNum(chars[1]) << 8) + (GetIndexNum(chars[2]) << 4) + GetIndexNum(chars[3]) + NumSum3;

        if (len == 5) return (GetIndexNum(chars[0]) << 16) + (GetIndexNum(chars[1]) << 12) + (GetIndexNum(chars[2]) << 8) + (GetIndexNum(chars[3]) << 4) + GetIndexNum(chars[4]) + NumSum4;

        if (len == 6) return (GetIndexNum(chars[0]) << 20) + (GetIndexNum(chars[1]) << 16) + (GetIndexNum(chars[2]) << 12) + (GetIndexNum(chars[3]) << 8) + (GetIndexNum(chars[4]) << 4) + GetIndexNum(chars[5]) + NumSum5;

        if (len == 7) return (GetIndexNum(chars[0]) << 24) + (GetIndexNum(chars[1]) << 20) + (GetIndexNum(chars[2]) << 16) + (GetIndexNum(chars[3]) << 12) + (GetIndexNum(chars[4]) << 8) + (GetIndexNum(chars[5]) << 4) + GetIndexNum(chars[6]) + NumSum6;

        return checked((GetIndexNum(chars[0]) << 28) + (GetIndexNum(chars[1]) << 24) + (GetIndexNum(chars[2]) << 20) + (GetIndexNum(chars[3]) << 16) + (GetIndexNum(chars[4]) << 12) + (GetIndexNum(chars[5]) << 8) + (GetIndexNum(chars[6]) << 4) + GetIndexNum(chars[7]) + NumSum7);
    }

    public static Int32 GetLength(UInt32 value)
    {
        if (value < NumCount1) return 1;

        if (value < NumSum2) return 2;

        if (value < NumSum3) return 3;

        if (value < NumSum4) return 4;

        if (value < NumSum5) return 5;

        if (value < NumSum6) return 6;

        if (value < NumSum7) return 7;

        return 8;
    }

    public static UInt32 GetIndexNum(Char ch) => ch switch
    {
        '0' => 0,
        '1' => 1,
        '2' => 2,
        '3' => 3,
        '4' => 4,
        '5' => 5,
        '6' => 6,
        '7' => 7,
        '8' => 8,
        '9' => 9,
        'a' or 'A' => 10,
        'b' or 'B' => 11,
        'c' or 'C' => 12,
        'd' or 'D' => 13,
        'e' or 'E' => 14,
        'f' or 'F' => 15,
        _ => throw new ArgumentOutOfRangeException(nameof(ch), $"Char '{ch}' is not HEX")
    };

    public static Byte GetIndexNumByte(Char ch) => ch switch
    {
        '0' => 0,
        '1' => 1,
        '2' => 2,
        '3' => 3,
        '4' => 4,
        '5' => 5,
        '6' => 6,
        '7' => 7,
        '8' => 8,
        '9' => 9,
        'a' or 'A' => 10,
        'b' or 'B' => 11,
        'c' or 'C' => 12,
        'd' or 'D' => 13,
        'e' or 'E' => 14,
        'f' or 'F' => 15,
        _ => throw new ArgumentOutOfRangeException(nameof(ch), $"Char '{ch}' is not HEX")
    };

    #endregion

    //static Hex()
    //{
    //    var mapL = Lower32._map;
    //    var mapU = Upper32._map;

    //    var maxL = mapL.Max();
    //    var maxU = mapL.Max();
    //    var len = maxU > maxL ? maxU : maxL;

    //    if (len > Int32.MaxValue) throw new InvalidOperationException();

    //    var map = new int[len + 1];

    //    for (int i = 0; i < map.Length; i++)
    //    {
    //        map[i] = -1;
    //    }

    //    for (var i = 0; i <= 255; i++)
    //    {
    //        var lower = (int)mapL[i];
    //        var upper = (int)mapU[i];

    //        map[lower] = i;
    //        map[upper] = i;

    //        //if (lower == upper)
    //        //    Console.WriteLine($"{lower} => {i},");
    //        //else
    //        //    Console.WriteLine($"{lower} or {upper} => {i},");
    //    }
    //    //Console.WriteLine("{");
    //    //for (int i = 0; i < 256; i++)
    //    //{
    //    //    //if (i == 103) break;

    //    //    var code = i < 103 ? DecodeMap[i] : -1;

    //    //    if (code == -1)
    //    //        Console.WriteLine($"255, //{i}");
    //    //    else
    //    //        Console.WriteLine($"{code,3}, //{i} -> {(char)i}");
    //    //}
    //    //Console.WriteLine("}");
    //}

    private static uint[] CreateLookup32Unsafe(string format)
    {
        var result = new uint[256];
        for (int i = 0; i < 256; i++)
        {
            string s = i.ToString(format);
            if (BitConverter.IsLittleEndian)
                result[i] = s[0] + ((uint)s[1] << 16);
            else
                result[i] = s[1] + ((uint)s[0] << 16);
        }
        return result;
    }

    private static ushort[] CreateLookup16Unsafe(string format)
    {
        var result = new ushort[256];
        for (int i = 0; i < 256; i++)
        {
            string s = i.ToString(format);
            if (BitConverter.IsLittleEndian)
                result[i] = (ushort)(s[0] + (s[1] << 8));
            else
                result[i] = (ushort)(s[1] + (s[0] << 8));
        }
        return result;
    }

    //public static void Decode(ReadOnlySpan<char> chars, Span<byte> bytes)
    //{
    //    bytes[0] = (byte)((_charToHex[chars[0]] << 4) | _charToHex[chars[1]]);
    //    bytes[1] = (byte)((_charToHex[chars[2]] << 4) | _charToHex[chars[3]]);
    //    bytes[2] = (byte)((_charToHex[chars[4]] << 4) | _charToHex[chars[5]]);
    //    bytes[3] = (byte)((_charToHex[chars[6]] << 4) | _charToHex[chars[7]]);
    //    bytes[4] = (byte)((_charToHex[chars[8]] << 4) | _charToHex[chars[9]]);
    //    bytes[5] = (byte)((_charToHex[chars[10]] << 4) | _charToHex[chars[11]]);
    //    bytes[6] = (byte)((_charToHex[chars[12]] << 4) | _charToHex[chars[13]]);
    //    bytes[7] = (byte)((_charToHex[chars[14]] << 4) | _charToHex[chars[15]]);
    //    bytes[8] = (byte)((_charToHex[chars[16]] << 4) | _charToHex[chars[17]]);
    //    bytes[9] = (byte)((_charToHex[chars[18]] << 4) | _charToHex[chars[19]]);
    //    bytes[10] = (byte)((_charToHex[chars[20]] << 4) | _charToHex[chars[21]]);
    //    bytes[11] = (byte)((_charToHex[chars[22]] << 4) | _charToHex[chars[23]]);
    //}

    //public static void Decode(ReadOnlySpan<byte> chars, Span<byte> bytes)
    //{
    //    bytes[0] = (byte)((_charToHex[chars[0]] << 4) | _charToHex[chars[1]]);
    //    bytes[1] = (byte)((_charToHex[chars[2]] << 4) | _charToHex[chars[3]]);
    //    bytes[2] = (byte)((_charToHex[chars[4]] << 4) | _charToHex[chars[5]]);
    //    bytes[3] = (byte)((_charToHex[chars[6]] << 4) | _charToHex[chars[7]]);
    //    bytes[4] = (byte)((_charToHex[chars[8]] << 4) | _charToHex[chars[9]]);
    //    bytes[5] = (byte)((_charToHex[chars[10]] << 4) | _charToHex[chars[11]]);
    //    bytes[6] = (byte)((_charToHex[chars[12]] << 4) | _charToHex[chars[13]]);
    //    bytes[7] = (byte)((_charToHex[chars[14]] << 4) | _charToHex[chars[15]]);
    //    bytes[8] = (byte)((_charToHex[chars[16]] << 4) | _charToHex[chars[17]]);
    //    bytes[9] = (byte)((_charToHex[chars[18]] << 4) | _charToHex[chars[19]]);
    //    bytes[10] = (byte)((_charToHex[chars[20]] << 4) | _charToHex[chars[21]]);
    //    bytes[11] = (byte)((_charToHex[chars[22]] << 4) | _charToHex[chars[23]]);
    //}

    //public static bool TryDecodeFromUtf16(ReadOnlySpan<char> chars, Span<byte> bytes)
    //{
    //    var byteHi = FromChar(chars[0]);
    //    var byteLo = FromChar(chars[1]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[0] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[2]);
    //    byteLo = FromChar(chars[3]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[1] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[4]);
    //    byteLo = FromChar(chars[5]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[2] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[6]);
    //    byteLo = FromChar(chars[7]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[3] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[8]);
    //    byteLo = FromChar(chars[9]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[4] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[10]);
    //    byteLo = FromChar(chars[11]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[5] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[12]);
    //    byteLo = FromChar(chars[13]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[6] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[14]);
    //    byteLo = FromChar(chars[15]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[7] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[16]);
    //    byteLo = FromChar(chars[17]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[8] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[18]);
    //    byteLo = FromChar(chars[19]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[9] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[20]);
    //    byteLo = FromChar(chars[21]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[10] = (byte)((byteHi << 4) | byteLo);

    //    byteHi = FromChar(chars[22]);
    //    byteLo = FromChar(chars[23]);

    //    if ((byteLo | byteHi) == 0xFF) return false;

    //    bytes[11] = (byte)((byteHi << 4) | byteLo);

    //    return true;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static int FromChar(int c)
    //{
    //    return c >= _charToHex.Length ? 0xFF : _charToHex[c];
    //}

    //{
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 15
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 31
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 47
    //        0x0,  0x1,  0x2,  0x3,  0x4,  0x5,  0x6,  0x7,  0x8,  0x9,  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 63
    //        0xFF, 0xA,  0xB,  0xC,  0xD,  0xE,  0xF,  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 79
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 95
    //        0xFF, 0xa,  0xb,  0xc,  0xd,  0xe,  0xf,  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 111
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 127
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 143
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 159
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 175
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 191
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 207
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 223
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 239
    //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF  // 255
    //};

    //public static readonly byte[] _DecodeMap = new byte[] {
    //255, //0
    //255, //1
    //255, //2
    //255, //3
    //255, //4
    //255, //5
    //255, //6
    //255, //7
    //255, //8
    //255, //9
    //255, //10
    //255, //11
    //255, //12
    //255, //13
    //255, //14
    //255, //15
    //255, //16
    //255, //17
    //255, //18
    //255, //19
    //255, //20
    //255, //21
    //255, //22
    //255, //23
    //255, //24
    //255, //25
    //255, //26
    //255, //27
    //255, //28
    //255, //29
    //255, //30
    //255, //31
    //255, //32
    //255, //33
    //255, //34
    //255, //35
    //255, //36
    //255, //37
    //255, //38
    //255, //39
    //255, //40
    //255, //41
    //255, //42
    //255, //43
    //255, //44
    //255, //45
    //255, //46
    //255, //47
    //  0, //48 -> 0
    //  1, //49 -> 1
    //  2, //50 -> 2
    //  3, //51 -> 3
    //  4, //52 -> 4
    //  5, //53 -> 5
    //  6, //54 -> 6
    //  7, //55 -> 7
    //  8, //56 -> 8
    //  9, //57 -> 9
    //255, //58
    //255, //59
    //255, //60
    //255, //61
    //255, //62
    //255, //63
    //255, //64
    // 10, //65 -> A
    // 11, //66 -> B
    // 12, //67 -> C
    // 13, //68 -> D
    // 14, //69 -> E
    // 15, //70 -> F
    //255, //71
    //255, //72
    //255, //73
    //255, //74
    //255, //75
    //255, //76
    //255, //77
    //255, //78
    //255, //79
    //255, //80
    //255, //81
    //255, //82
    //255, //83
    //255, //84
    //255, //85
    //255, //86
    //255, //87
    //255, //88
    //255, //89
    //255, //90
    //255, //91
    //255, //92
    //255, //93
    //255, //94
    //255, //95
    //255, //96
    // 10, //97 -> a
    // 11, //98 -> b
    // 12, //99 -> c
    // 13, //100 -> d
    // 14, //101 -> e
    // 15, //102 -> f
    //255, //103
    //255, //104
    //255, //105
    //255, //106
    //255, //107
    //255, //108
    //255, //109
    //255, //110
    //255, //111
    //255, //112
    //255, //113
    //255, //114
    //255, //115
    //255, //116
    //255, //117
    //255, //118
    //255, //119
    //255, //120
    //255, //121
    //255, //122
    //255, //123
    //255, //124
    //255, //125
    //255, //126
    //255, //127
    //255, //128
    //255, //129
    //255, //130
    //255, //131
    //255, //132
    //255, //133
    //255, //134
    //255, //135
    //255, //136
    //255, //137
    //255, //138
    //255, //139
    //255, //140
    //255, //141
    //255, //142
    //255, //143
    //255, //144
    //255, //145
    //255, //146
    //255, //147
    //255, //148
    //255, //149
    //255, //150
    //255, //151
    //255, //152
    //255, //153
    //255, //154
    //255, //155
    //255, //156
    //255, //157
    //255, //158
    //255, //159
    //255, //160
    //255, //161
    //255, //162
    //255, //163
    //255, //164
    //255, //165
    //255, //166
    //255, //167
    //255, //168
    //255, //169
    //255, //170
    //255, //171
    //255, //172
    //255, //173
    //255, //174
    //255, //175
    //255, //176
    //255, //177
    //255, //178
    //255, //179
    //255, //180
    //255, //181
    //255, //182
    //255, //183
    //255, //184
    //255, //185
    //255, //186
    //255, //187
    //255, //188
    //255, //189
    //255, //190
    //255, //191
    //255, //192
    //255, //193
    //255, //194
    //255, //195
    //255, //196
    //255, //197
    //255, //198
    //255, //199
    //255, //200
    //255, //201
    //255, //202
    //255, //203
    //255, //204
    //255, //205
    //255, //206
    //255, //207
    //255, //208
    //255, //209
    //255, //210
    //255, //211
    //255, //212
    //255, //213
    //255, //214
    //255, //215
    //255, //216
    //255, //217
    //255, //218
    //255, //219
    //255, //220
    //255, //221
    //255, //222
    //255, //223
    //255, //224
    //255, //225
    //255, //226
    //255, //227
    //255, //228
    //255, //229
    //255, //230
    //255, //231
    //255, //232
    //255, //233
    //255, //234
    //255, //235
    //255, //236
    //255, //237
    //255, //238
    //255, //239
    //255, //240
    //255, //241
    //255, //242
    //255, //243
    //255, //244
    //255, //245
    //255, //246
    //255, //247
    //255, //248
    //255, //249
    //255, //250
    //255, //251
    //255, //252
    //255, //253
    //255, //254
    //255, //255
    //};
}