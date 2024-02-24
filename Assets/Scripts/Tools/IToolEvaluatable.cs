namespace VisualizationTool.Tools
{
    interface IToolEvaluatable
    {
        void EvaluateChildrenAndSelf();
        void EvaluateChildren();
        void EvaluateChildren(ToolBase instrument);
    }
}