调用示例：
new GTA.FileCenter.ConvertFile().Convert(@"C:\Users\dongli.li\Desktop\新建文件夹\原文件.pptx");

所需配置：
<!-- 转换工具目录 -->
<add key="toolpath" value="E:\SVN\GTA_UTP\GTA.UTP.PluginMvcWeb\"/>
<!-- PDF转Flash工具路径 -->
<add key="pdf2swfexe" value="SWFTools\pdf2swf.exe"/>
<!-- SWF转缩略图工具路径 -->
<add key="swfrenderexe" value="SWFTools\swfrender.exe"/>
<!-- PDF文件解析 -->
<add key="xpdfdir" value="xpdf\xpdf-chinese-simplified"/>
<!-- 文件转换支持的格式 -->
<add key="supportFormat" value=".doc,.docx,.xls,.xlsx,.ppt,.pptx,.pdf"/>
<!-- 缩略图 宽 -->
<add key="ThumbW" value="150"/>
<!-- 缩略图 高 -->
<add key="ThumbH" value="150"/>