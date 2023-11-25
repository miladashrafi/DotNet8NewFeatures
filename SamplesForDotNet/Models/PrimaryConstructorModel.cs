namespace DotNet8NewFeatures.Models
{
    public class PrimaryConstructorModel(int number)
    {
        public void increment() => number++;
    }

    public class OldConstructorModel
    {
        private int _d41d8cd98f00b204e9800998ecf8427e_number;
        public OldConstructorModel(int number)
        {
            _d41d8cd98f00b204e9800998ecf8427e_number = number;
        }
        public int increment() => _d41d8cd98f00b204e9800998ecf8427e_number++;
    }

    public class PrimaryConstructorModel2(int number) : PrimaryConstructorModel(number)
    {
        public PrimaryConstructorModel2() : this(12)
        {

        }
    }
}
