using System;
//diagram logistic or stryktyra classes
namespace Project1
{
    //------------------Decorator--------------------------
    public interface IText
    {
        string GetText(); 
    }
    public class SimpleText : IText
    {
        private string _text;
        public SimpleText(string text)
        {
            _text = text;
        }
        public string GetText()
        {
            return _text;
        }
    }
    public abstract class TextDecorator : IText
    {
        protected IText text1;
        public TextDecorator(IText text)
        {
            text1 = text;
        }
        public abstract string GetText();
    }
    public class BoldText : TextDecorator
    {
        public BoldText(IText text) : base(text) { }
        public override string GetText()
        {
            return $"<b>{text1.GetText()}</b>";
        }
    }
    public class UnderLineText : TextDecorator
    {
        public UnderLineText(IText text) : base(text) { }
        public override string GetText()
        {
            return $"<u>{text1.GetText()}</u>";
        }
    }
    //------------------Decorator--------------------------
    //------------------Stratagy---------------------------
    public interface ITextFormatter
    {
        public string Format(string text); 
    }
    public class UpperFormatter : ITextFormatter
    {
        public string Format(string text)
        {
            return text.ToUpper();
        }
    }
    public class LowerFormatter : ITextFormatter
    {
        public string Format(string text)
        {
            return text.ToLower();
        }
    }
    //------------------Stratagy---------------------------
    //------------------C.O.R.-----------------------------
    public interface ITextHandle
    {
        void SetNext(ITextHandle Handle);
        void Handle(IText text);
    }
    public abstract class TextHandle : ITextHandle
    {
        protected ITextHandle _textHandle;
        public void SetNext(ITextHandle textHandle)
        {
            _textHandle = textHandle;
        }
        public virtual void Handle(IText text)
        {
            if(_textHandle != null)
            {
                _textHandle.Handle(text);
            }
        }
    }
    public class BoldHandler : TextHandle
    {
        public override void Handle(IText text)
        {
            if (text is BoldText)
            {
                Console.WriteLine(text.GetText());
            }
            else
            {
                base.Handle(text);
            }
        }
    }
    public class UnderlineHandle : TextHandle
    {
        public override void Handle(IText text)
        {
            if(text is UnderLineText)
            {
                Console.WriteLine(text.GetText());
            }
            else { base.Handle(text); }
        }
    }
    //------------------C.O.R.-----------------------------
    public class Obj
    {
        public static void Main(string[] args)
        {
            IText text = new SimpleText("Hello World");

            IText boldtext = new BoldText(text);
            IText underlinetext = new UnderLineText(text);

            ITextHandle boldHandle = new BoldHandler();
            ITextHandle underlineHandler = new UnderlineHandle();

            boldHandle.SetNext(underlineHandler);

            boldHandle.Handle(boldtext);
            boldHandle.Handle(underlinetext);
            boldHandle.Handle(text);
        }
    }
}