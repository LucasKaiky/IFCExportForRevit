using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace RevitIFCExport
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class IFCExport : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document doc = commandData?.Application?.ActiveUIDocument?.Document;

                if (doc == null)
                {
                    TaskDialog.Show("Erro", "Nenhum documento do Revit está ativo.");
                    return Result.Failed;
                }

                using (Transaction transaction = new Transaction(doc, "Exportar para IFC"))
                {
                    transaction.Start();

                    string ifcFilePath = "C:\\Users\\lucas\\Downloads";
                    string ifcTitle = doc.Title;
                    IFCExportOptions options = new IFCExportOptions();
                    doc.Export(ifcFilePath, ifcTitle, options);
                    transaction.Commit();
                }

                TaskDialog.Show("IFC Export", "Exportação para IFC concluída com sucesso!");

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Erro", "Erro ao exportar para IFC: " + ex.Message);
                return Result.Failed;
            }
        }
    }
}

