using System;
//diagram logistic or stryktyra classes
namespace Project
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
        public static void Main1(string[] args)
        {
            //IText text = new SimpleText("Hello World!");
            //IText boldtext = new BoldText(text);
            //IText underlinetext = new UnderLineText(boldtext);
            //Console.WriteLine(underlinetext.GetText());
            //ITextFormatter textFormatter = new LowerFormatter();


            //string formatedtext = textFormatter.Format(text.GetText());
            //Console.WriteLine(formatedtext);
            int choice, choice_;
            string text_to_write;
            Console.WriteLine("Enter text: ");
            text_to_write = Console.ReadLine();
            do
            {
                Console.WriteLine("Choose what operation you want to use(1 - Bold text, 2 - Lower text, 3 - Underline text, 4 - Upper text, 5 - Chain of responsibility):");
                choice = Convert.ToInt32(Console.ReadLine());
                IText text_to_use = new SimpleText(text_to_write);
                switch (choice)
                {
                    case 1:
                        IText bold_text = new BoldText(text_to_use);
                        Console.WriteLine(bold_text.GetText());
                        break;
                    case 2:
                        ITextFormatter textFormatter_L = new LowerFormatter();
                        string formatedtext_L = textFormatter_L.Format(text_to_use.GetText());
                        Console.WriteLine(formatedtext_L);
                        break;
                    case 3:
                        IText undelined_text = new UnderLineText(text_to_use);
                        Console.WriteLine(undelined_text.GetText());
                        break;
                    case 4:
                        ITextFormatter textFormatter_U = new UpperFormatter();
                        string formatedtext_U = textFormatter_U.Format(text_to_use.GetText());
                        Console.WriteLine(formatedtext_U);
                        break;
                    case 5:
                        do
                        {
                            Console.WriteLine("Choose what operation you want to use(1 - Bold text, 2 - Underline text, 3 - Default text):");
                            choice_ = Convert.ToInt32(Console.ReadLine());
                            ITextHandle boldHandle = new BoldHandler();
                            ITextHandle underlineHandle = new UnderlineHandle();
                            boldHandle.SetNext(underlineHandle);
                            switch (choice_)
                            {
                                case 1:
                                    IText bold_text_ = new BoldText(text_to_use);
                                    boldHandle.Handle(bold_text_);
                                    break;
                                case 2:
                                    IText undelined_text_ = new UnderLineText(text_to_use);
                                    boldHandle.Handle(undelined_text_);
                                    break;
                                case 3:
                                    boldHandle.Handle(text_to_use);
                                    break;
                                case 0:
                                    break;
                            }
                        } while (choice_ != 0) ;
                        break;
                    case 0:
                        break;
                }
            } while (choice != 0);
        }
    }
}