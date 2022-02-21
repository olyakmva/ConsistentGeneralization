
namespace AlgorithmsLibrary
{
    public static class AlgmFabrics
    {
        public static ISimplificationAlgm GetAlgmByNameAndParam(string algmName)
        {
            ISimplificationAlgm algm = null;
            switch (algmName)
            {
                case "DouglasPeuckerAlgm":
                    algm = new DouglasPeuckerAlgm();
                    break;
                case "LiOpenshawAlgm":
                    algm = new LiOpenshawAlgm();
                    break; 
                case "VisvWhyattAlgm": algm= new VisWhyattAlgmWithTolerance();
                    break;
                case "SleeveFitAlgm":
                    algm = new SleeveFitAlgm();
                    break;
            }
            return algm;

        }
    }
}
