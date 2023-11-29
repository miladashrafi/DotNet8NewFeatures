namespace DotNet8NewFeatures.Models
{
    public class PrimaryConstructorModel(int number)
    {
        public int Increment() => number++;
    }

    public class OldConstructorModel
    {
        private int _number;
        public OldConstructorModel(int number)
        {
            _number = number;
        }
        public int Increment() => _number++;
    }

    public class ConvertedConstructorModel
    {
        private int _d41d8cd98f00b204e9800998ecf8427e_number;
        public ConvertedConstructorModel(int number)
        {
            _d41d8cd98f00b204e9800998ecf8427e_number = number;
        }
        public int Increment() => _d41d8cd98f00b204e9800998ecf8427e_number++;
    }

    public class PrimaryConstructorModel2(int number) : PrimaryConstructorModel(number)
    {
        public PrimaryConstructorModel2() : this(12)
        {

        }
    }
}
