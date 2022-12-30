using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace labtest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region inp_way_to_labs

            Console.Write("Введите путь к папке с лабораторными работами:");
            string waytolabs = Console.ReadLine();

            #endregion

            #region inp_data_students

            //потом хочется получать names.txt из названий папок в общей папке со всеми
            HashSet<string> names = new HashSet<string>();
            string path = "names.txt";
            string[] inplines = System.IO.File.ReadAllLines(path);

            foreach (var item in inplines) names.Add(item);

            #endregion

            #region test_all_labs

            string stringresoult = "";
            for (int i = 0; i < 1; i++)

                foreach (var name in names)
                {
                    #region input_test

                    //как можно в каждом в цикле for по i получать переменную "test_i" ,кроме массива

                    string[] rightinp = System.IO.File.ReadAllLines($"{waytolabs}/{name}/input{i}.txt");
                    string[] rightoutput = System.IO.File.ReadAllLines($"{waytolabs}/{name}/true_out{i}.txt");

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

                    path = "@resoultoftests.txt";
                    stringresoult += name + " test" + Convert.ToString(i) + " " + Convert.ToString(correct) + "\n";

                    #endregion
                }

            File.WriteAllText(path, stringresoult);

            #endregion
        }
    }
}
