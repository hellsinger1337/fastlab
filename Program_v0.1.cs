using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;

namespace labtest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region inp_way_to_labs

            Console.Write("Введите путь к папке с лабораторными работами:");
            string waytolabsbase = Console.ReadLine();
            HashSet<string> labs = new HashSet<string>();
            string path = "";
            string stringresoult = "";
            DirectoryInfo dd = new DirectoryInfo(waytolabsbase);
            foreach (var item in dd.GetDirectories())
                labs.Add(Convert.ToString(item));
            foreach (var lab in labs)
            {
                string waytolabs = $"{waytolabsbase}/{lab}";
                #endregion

                #region inp_data_students
                HashSet<string> names = new HashSet<string>();
                DirectoryInfo dir = new DirectoryInfo(waytolabs);
                foreach (var item in dir.GetDirectories())
                    names.Add(Convert.ToString(item));


                #endregion

                #region test_all_labs



                for (int i = 0; i < 1; i++)
                {
                    #region get_input
                    string[] rightinp = System.IO.File.ReadAllLines($"{waytolabs}/input{i}.txt");
                    string[] rightoutput = new string[0];
                    #endregion
                    foreach (var name in names)
                    {
                        #region get_first_output

                        if (rightoutput.Length == 0)
                        {
                            #region Find_Executable

                            string[] files1 = System.IO.Directory.GetFiles($"{waytolabs}/{name}/", "*.exe");

                            #endregion
                            #region run_lab1

                            var pInfo1 = new ProcessStartInfo();
                            pInfo1.FileName = files1[0];
                            pInfo1.RedirectStandardInput = true;
                            pInfo1.RedirectStandardOutput = true;
                            pInfo1.UseShellExecute = false;

                            var labProcess1 = Process.Start(pInfo1);

                            for (int j = 0; j != rightinp.Length; j++)
                                labProcess1.StandardInput.WriteLine(rightinp[j]);

                            rightoutput = new string[rightoutput.Length];
                            for (int j = 0; j < rightoutput.Length; j++)
                                rightoutput[j] = labProcess1.StandardOutput.ReadLine();

                            #endregion
                        }

                        #endregion

                        #region Find_Executable

                        string[] files = System.IO.Directory.GetFiles($"{waytolabs}/{name}/", "*.exe");

                        #endregion

                        #region run_lab

                        var pInfo = new ProcessStartInfo();
                        pInfo.FileName = files[0];
                        pInfo.RedirectStandardInput = true;
                        pInfo.RedirectStandardOutput = true;
                        pInfo.UseShellExecute = false;

                        var labProcess = Process.Start(pInfo);

                        for (int j = 0; j != rightinp.Length; j++)
                            labProcess.StandardInput.WriteLine(rightinp[j]);

                        string[] realoutput = new string[rightoutput.Length];
                        for (int j = 0; j < realoutput.Length; j++)
                            realoutput[j] = labProcess.StandardOutput.ReadLine();

                        #endregion

                        #region correct_or_not

                        bool correct = true;
                        for (int j = 0; j != realoutput.Length; j++)
                            if (realoutput[j] != rightoutput[j]) correct = false;

                        #endregion

                        #region write_resoult

                        path = "resoultoftests.txt";
                        stringresoult += $"{lab} {name} test{i} {correct}; ";

                        #endregion
                    }
                    stringresoult += "\n";
                }
                #endregion
            }
            File.WriteAllText(path, stringresoult);
        }
    }
}
