using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MagicWordCounter_CodeBehind_Style
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string TextToCount {
            get
            {
                return txtToCount.Text;
            }
            set
            {
                txtToCount.Text = value;
            }
        }

        public bool ExcludeArticles
        {
            get
            {
                return chbExcludeArticles.IsChecked.Value;
            }
            set
            {
                chbExcludeArticles.IsChecked = value;
            }
        }

        public bool CountQuotesAsOneWord
        {
            get
            {
                return chbQuotes.IsChecked.Value;
            }
            set
            {
                chbQuotes.IsChecked = value;
            }
        }

        public string LabelText
        {
            get
            {
                return (string)lblCount.Content;
            }
        }

        private bool isArticle(string word)
        {
            return word.ToLower() == "a" || word.ToLower() == "an" || word.ToLower() == "the";
        }

        /// <summary>
        /// Regular expression that matches text inside quotation marks (including the quotation marks), taking into account smart quotes.
        /// </summary>
        private static Regex QuoteRegex => new Regex($"(\\\"|\\{(char)0x201C}|\\{(char)0x201D})((.|\n)*?)(\\\"|\\{(char)0x201C}|\\{(char)0x201D})", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public void btnCount_Click(object sender, RoutedEventArgs e)
        {
            var wordCount = 0;

            // Strip out new lines and elipses (otherwise "some quote…is split" counts as 3 words)
            var textToCount = txtToCount.Text.Replace(Environment.NewLine, " ").Replace("…", " ");            

            // Count words, taking into account article adjective settings
            foreach (var word in textToCount.Split(' '))
            {
                // Increment word count IF
                if (!string.IsNullOrEmpty(word) && // word is not null or empty.
                        (!isArticle(word.Trim('\"')) || // AND (either word is not an article adjective.  // The trim is to remove quotation marks from articles.  `This is "a sentence"` would count `"a` as a word
                        !chbExcludeArticles.IsChecked.Value)) // OR article exclusion is disabled)
                {
                    wordCount += 1;
                }
            }

            // Take into account quotations
            if (chbQuotes.IsChecked.Value)
            {
                foreach (Match match in QuoteRegex.Matches(textToCount))
                {
                    // Count the words inside the quotation marks
                    var quoteCount = 0;
                    foreach (var word in match.Groups[2].Value.Split(' '))
                    {
                        if (!string.IsNullOrEmpty(word) && (!isArticle(word) || !chbExcludeArticles.IsChecked.Value))
                        {
                            quoteCount += 1;
                        }
                    }

                    // Because the words inside the quote have already been counted in wordCount, subtract the number of words in the quote from wordCount
                    wordCount -= (quoteCount - 1); //The "- 1" part makes it so that quotes count as 1 word, instead of 0
                }
            }

            lblCount.Content = $"{wordCount} word(s)";
        }
    }
}
