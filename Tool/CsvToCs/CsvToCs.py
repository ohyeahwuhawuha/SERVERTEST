#!/usr/bin/python
# -*- coding: UTF-8 -*- 

import codecs
import sys
import getopt

FileName = ""
CSPath = ""
CSVPath = ""

try:
    opts, args = getopt.getopt(sys.argv[1:], "i:o:f:", ["input=", "output=", "file="])
except getopt.GetoptError:          
    usage()                         
    sys.exit(2)  
for opt, arg in opts:
    if opt in ("-i", "--input"):
        CSVPath = arg
    elif opt in ("-o", "--output"):
        CSPath = arg
    elif opt in ("-f", "--file"):
        FileName = arg

#print(FileName)
#print(CSPath)
#print(CSVPath)

filePath = CSVPath + "\\%s.csv"%(FileName)
outPath = CSPath + "\\CF_%s.cs"%(FileName)

modelFile = codecs.open(filePath,"r")
print("in :" + modelFile.name)

mLine = modelFile.readline()
#print(mLine)

mLine = modelFile.readline()
#print(mLine)
mRealLine = mLine.split("\n")
mType = mRealLine[0].split(",")

mLine = modelFile.readline()
#print(mLine)
mRealLine = mLine.split("\n")
mVarName = mRealLine[0].split(",")

#定义变量
content0 = ""

for mIndex in range(0, len(mVarName)):
    con = "        public %s %s;\n"%(mType[mIndex], mVarName[mIndex])
    content0 += con

#初始化变量
content1 = ""

typeString = u'string'
typeInt = u'int'
typeUInt = u'uint'
typeFloat = u'float'

for mIndex in range(0, len(mVarName)):
    if mType[mIndex] == typeString:
        con = "            %s = string.Empty;\n"%(mVarName[mIndex])
    else :
        if mType[mIndex] == typeInt:
            con = "            %s = 0;\n"%(mVarName[mIndex])
        else :
            if mType[mIndex] == typeUInt:
                con = "            %s = 0u;\n"%(mVarName[mIndex])
            else :
                if mType[mIndex] == typeFloat:
                    con = "            %s = 0f;\n"%(mVarName[mIndex])
                else:    
                    con = "            %s = null;\n"%(mVarName[mIndex])                       
    content1 += con

#解析数据
content2 = ""
for mIndex in range(0, len(mVarName)):
    if mType[mIndex] == typeString:
        con = "        date.%s = vars[%d];\n"%(mVarName[mIndex], mIndex)
    else:
        con = """
        if(!%s.TryParse(vars[%d], out date.%s))
            PrintError(GetFileName(), line, %d);\n"""%(mType[mIndex], mIndex, mVarName[mIndex], mIndex)
    content2 += con

con = "        m_dateTable[date.%s] = date;"%(mVarName[0])
content2 += con

#数据转成字符串
content3 = ""
for mIndex in range(0, len(mVarName)):
    if mType[mIndex] == typeString:
        if mIndex < len(mVarName) - 1:
            con = "%s + \",\" + "%(mVarName[mIndex])
        else:
            con = "%s"%(mVarName[mIndex])        
    else:
        if mType[mIndex] == typeInt or mType[mIndex] == typeUInt or mType[mIndex] == typeFloat:
            if mIndex < len(mVarName) - 1:
                con = "%s.ToString() + \",\" + "%(mVarName[mIndex])
            else:
                con = "%s.ToString()"%(mVarName[mIndex])
        else:
            if mIndex < len(mVarName) - 1:
                con = "%s.ToString(%s, \"0\") + \",\" + "%(mType[mIndex], mVarName[mIndex])
            else:
                con = "%s.ToString(%s, \"0\")"%(mType[mIndex], mVarName[mIndex])                             
    content3 += con
    
content = """using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FileHelper;

public class CF_%s : CSVFileRead<CF_%s>
{
    private Hashtable m_dateTable = new Hashtable();
    public Hashtable DateTable
    {
        get
        {
            return m_dateTable;
        }
    }
      
    public override string GetFileName()
    {
        return "%s";
    }

    public class DataEntry
    {
%s
        public DataEntry()
        {
%s
        }
        
        public string ToLine()
        {
            return %s + "\\n";
        }
    }

    protected override void ParseCsvDate(string[] vars, int line)
    {
        if (vars == null)
            return;

        CF_%s.DataEntry date = new CF_%s.DataEntry();
%s
    }

    public CF_%s.DataEntry GetDataEntryPtr(uint id)
    {
        return m_dateTable[id] as CF_%s.DataEntry;
    }
    
    public string ToCsv()
	{
		if (DateTable == null)
			return null;

		string csv = null;
		foreach (DataEntry date in DateTable)
		{
			csv += date.ToLine ();	
		}
		return csv;
	}
}"""%(FileName, FileName, FileName, content0 ,content1, content3, FileName, FileName, content2, FileName, FileName)

#print (content)

outFile = codecs.open(outPath, 'w', "UTF-8")
outFile.write(content)
outFile.close()
print("out :" + outPath)
print("Finish " + FileName)

