using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalFileCreation
{
    class JournalCommand
    {
        public string[] path = null;
        public String fileName = "";
        public String outPutDir = "";
        public String view = "3D View: {3D}";

        public JournalCommand(string[] fileNamepath, String outDir)
        {
            
            path = fileNamepath;
            outPutDir = outDir;
            view = "3D View: {3D}";
        }

        public List<String> GetCommand()
        {
            List<String> list = new List<string>();
            String f1 = CreateStartCommand();
            list.Add(f1);
            foreach (string item in path)
            {
                int x = item.LastIndexOf(@"\");
                String fileName = item.Substring(x + 1, item.Length - x - 1);
                String outPath = outPutDir + @"\" + fileName;
                String f2 = CreateCommandForRVTfileOpening(item);
               String f3 = CreateRuntheExternalCommand("-");
                String f4 = CreateRvtSaveAsCommand(outPath);
               // String f4 = CreateRvtSaveCommand();
                String f5 = CreateRvtCloseDocumentCommand(fileName, view);
               String command = f2 + f3 +f4 + f5;
               list.Add(command);
            }
            String f6 = CreateRvtCloseCommand();
            list.Add(f6);
            return list;
        }
        String CreateStartCommand()
        {
            /*
             * 'Autamtion for opening and close the .rvt files and run the external command the revit will close.
             * 'make sure that addin file aND JOURNAL FILE SHOULD BE SAME DIR 
             *  Dim Jrn
             * Set Jrn = CrsJournalScript
             */
            StringBuilder sb = new StringBuilder();
            sb.Append("'***\n'***\n\n");
            sb.Append("Dim Jrn \nSet Jrn = CrsJournalScript\n");
            return sb.ToString();
        }

        String CreateCommandForRVTfileOpening(String filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'*********************************************************************************\n");
            sb.Append("\nJrn.Command \"Ribbon\" , \"Open an existing project , ID_REVIT_FILE_OPEN\" \n");
            sb.Append("Jrn.Data \"File Name\"  _\n");
            sb.Append(" , \"IDOK\", \"");
            sb.Append(filePath).Append("\"\n");
            return sb.ToString();
        }

        String CreateRuntheExternalCommand(String commandId)
        {
            /*
              Jrn.RibbonEvent "TabActivated:Add-Ins"
              Jrn.RibbonEvent "Execute external command:CustomCtrl_%CustomCtrl_%Add-Ins%      3DIQ      %3DIQ Material Proto:HelloRevitCpp.Prototype"
              Jrn.Data "APIStringStringMapJournalData"  _
              , 0 
             Jrn.Data "Transaction Successful"  _
                       , "Pattern insert CLR"
           */
            commandId = "CustomCtrl_%CustomCtrl_%Add-Ins%      3DIQ      %3DIQ Material Proto:HelloRevitCpp.Prototype\"\n";
            StringBuilder sb = new StringBuilder();
            //String fileName = "";
            sb.Append("\nJrn.RibbonEvent \"TabActivated:Add-Ins\" \n");
            sb.Append("Jrn.RibbonEvent \"Execute external command:CustomCtrl_%CustomCtrl_%Add-Ins%      3DIQ      %3DIQ Material Proto:HelloRevitCpp.Prototype\"\n");
            sb.Append(" Jrn.Data \"APIStringStringMapJournalData\"  _\n, 0").Append("\n\n");
            return sb.ToString();
        }

        String CreateRvtSaveAsCommand(String path)
        {
            /*
                 Jrn.Command "Ribbon" , "Save the active project with a new name , ID_REVIT_FILE_SAVE_AS"
                 Jrn.Data "File Name"  _
                 , "IDOK", "C:\Users\Umesh Kumar\Desktop\new\Test case for planner.rvt"
           */
          
            StringBuilder sb = new StringBuilder();
            sb.Append("\nJrn.Command \"Ribbon\" , \"Save the active project with a new name , ID_REVIT_FILE_SAVE_AS\" \n");
            sb.Append("Jrn.Data \"File Name\"  _\n");
            sb.Append(" , \"IDOK\", \"");
            sb.Append(path).Append("\"\n");
            return sb.ToString();
        }
        String CreateRvtSaveCommand()
        {
            /*
          
                        Jrn.Command "AccelKey" , "Save the active project , ID_REVIT_FILE_SAVE"
             */
            StringBuilder sb = new StringBuilder();
            sb.Append("\n  Jrn.Command \"AccelKey\" , \"Save the active project , ID_REVIT_FILE_SAVE\" \n\n");
        
            return sb.ToString();
        }

        String CreateRvtCloseDocumentCommand(String fileName, String view)
        {
            /*
               Jrn.Close "[Test case for planner.rvt]" , "3D View: {3D}"
           */
            StringBuilder sb = new StringBuilder();
            sb.Append("\nJrn.Close \"[");
            sb.Append(fileName);
            sb.Append("]\" , ");
            sb.Append("\"").Append(view).Append("\"").Append("\n\n");
            sb.Append("'*********************************************************************************\n");
            return sb.ToString();
        }

        String CreateRvtCloseCommand()
        {
            /*
               
                Jrn.Command "SystemMenu" , "Quit the application; prompts to save projects , ID_APP_EXIT"

           */
            StringBuilder sb = new StringBuilder();
            sb.Append("\nJrn.Command \"SystemMenu\" , \"Quit the application; prompts to save projects , ID_APP_EXIT\"\n");
            return sb.ToString();
        }
        
    }
}
