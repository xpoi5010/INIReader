using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIReader.Data
{
    public class INIParse
    {
        private int Ln = 1;

        private int Col = 1;

        
       public INIData ParseINI(string INIContent, bool EscapeMode)
        {
            string Value = "";
            Stack<INICommandOpcode> stack = new Stack<INICommandOpcode>();
            INIData data = new INIData();
            INISection temp = new INISection();
            bool first = true;
            stack.Push(INICommandOpcode.None);
            INIContent += "\r\n";
            string NAME = "";
            foreach (char Cha in INIContent)
            {
                if (stack.Peek() == INICommandOpcode.STRING)
                {
                    if (Cha == '"')
                    {
                        stack.Pop();
                        continue;
                    }
                    else if (Cha == '\\' && EscapeMode)
                    {
                        stack.Push(INICommandOpcode.ESCAPE);
                        continue;
                    }
                    else
                    {
                        Value += Cha;
                        continue;
                    }
                }
                if (stack.Peek() == INICommandOpcode.None)
                {
                    if (Cha == '[')
                    {
                        stack.Push(INICommandOpcode.SECTION);
                        continue;
                    }
                    else if (Cha == ';')
                    {
                        stack.Push(INICommandOpcode.COMMENT);
                        continue;
                    }
                    else if (IsEngChar(Cha))
                    {
                        stack.Push(INICommandOpcode.NAME);
                        Value += Cha;
                        continue;
                    }
                }
                if (stack.Peek() == INICommandOpcode.SECTION)
                {
                    if (Cha == ']')
                    {
                        stack.Pop();
                        //output.Add(new INICommand(INICommandOpcode.SECTION, Value));
                        if (!first)
                            data.Add(temp);
                        else
                            first = false;
                        temp = new INISection(Value);
                        Value = "";
                        continue;
                    }
                    else if (Cha != '[' && Cha != '\r' && Cha != '\n')
                    {
                        Value += Cha;
                        continue;
                    }
                    continue;
                }
                if (stack.Peek() == INICommandOpcode.NAME)
                {
                    if (Cha == '=')
                    {
                        stack.Pop();
                        //output.Add(new INICommand(INICommandOpcode.NAME, Value.Trim()));
                        NAME = Value.Trim();
                        Value = "";
                        stack.Push(INICommandOpcode.WAITINGVALUE);
                        continue;
                    }
                    else
                    {
                        Value += Cha;
                        continue;
                    }
                }
                if (stack.Peek() == INICommandOpcode.WAITINGVALUE)
                {
                    if (Cha != ' ')
                    {
                        stack.Pop();
                        stack.Push(INICommandOpcode.VALUE);
                    }
                }
                if (stack.Peek() == INICommandOpcode.VALUE)
                {
                    if (Cha == '\n' || Cha == '\r')
                    {
                        stack.Pop();
                        //output.Add(new INICommand(INICommandOpcode.VALUE, Value));
                        temp.Add(new INIItem(NAME,Value));
                        Value = "";
                        continue;
                    }
                    else if (Cha == '"')
                    {
                        stack.Push(INICommandOpcode.STRING);
                        continue;
                    }
                    else if (Cha == ';')
                    {
                        stack.Pop();
                        temp.Add(new INIItem(NAME, Value));
                        Value = "";
                        stack.Push(INICommandOpcode.COMMENT);
                        continue;
                    }
                    Value += Cha;
                    continue;
                }
                if (stack.Peek() == INICommandOpcode.COMMENT)
                {
                    if (Cha == '\n' || Cha == '\r')
                    {
                        stack.Pop();
                        Value = "";
                        continue;
                    }
                    else
                    {
                        //Value += Cha;
                        continue;
                    }
                }
                if (stack.Peek() == INICommandOpcode.ESCAPE)
                {
                    if (Cha == 'n')
                    {
                        Value += "\n";
                    }
                    else if (Cha == 'r')
                    {
                        Value += "\r";
                    }
                    else if (Cha == '0')
                    {
                        Value += "\0";
                    }
                    else if (Cha == 't')
                    {
                        Value += "\t";
                    }
                    else if (Cha == 'b')
                    {
                        Value += "\b";
                    }
                    else if (Cha == 'f')
                    {
                        Value += "\f";
                    }
                    else if (Cha == '\'')
                    {
                        Value += "\'";
                    }
                    else if (Cha == '"')
                    {
                        Value += "\"";
                    }
                    else if (Cha == '\\')
                    {
                        Value += "\\";
                    }
                    stack.Pop();
                    continue;
                }
            }
            data.Add(temp);
            return data;
        }

        private bool IsEngChar(char Cha)
        {
            return (Cha >= 0x61 && Cha <= 0x7A) || (Cha >= 0x41 && Cha <= 0x5A);
        }
    }
}
