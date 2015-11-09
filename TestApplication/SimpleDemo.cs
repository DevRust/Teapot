using TestApplication.Entities;

namespace TestApplication
{
    public class SimpleDemo
    {
        private readonly IPot<Tea> _teapot;

        public SimpleDemo(IPot<Tea> teapot)
        {
            _teapot = teapot;
        }

        public void ImALittleTeapot(short stout)
        {
            _teapot.Find("Handle");
            _teapot.Find("Spout");

            _teapot.Add(new Tea());
        }
    }
}