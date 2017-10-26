namespace Sketchpad.Data.StatePattern
{
    class DoNothing: IChangeCanvasData
    {
        public void Change(CanvasData parameters)
        {
            return;
        }
    }
}
