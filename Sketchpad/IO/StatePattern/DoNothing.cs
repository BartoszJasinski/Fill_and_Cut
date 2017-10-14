namespace Sketchpad.IO.StatePattern
{
    class DoNothing: IChangeCanvasData
    {
        public void Change(CanvasData parameters)
        {
            return;
        }
    }
}
