using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// Generated with Gemini 2.0 Pro Experimental

namespace Common.Controls
{
    public class MaskedTextBox : TextBox
    {
        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.Register("Mask", typeof(string), typeof(MaskedTextBox), new PropertyMetadata(string.Empty, OnMaskChanged));

        public string Mask
        {
            get { return (string)GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }

        public static readonly DependencyProperty MaskCharProperty =
            DependencyProperty.Register("MaskChar", typeof(char), typeof(MaskedTextBox), new PropertyMetadata(' '));

        public char MaskChar
        {
            get { return (char)GetValue(MaskCharProperty); }
            set { SetValue(MaskCharProperty, value); }
        }

        public MaskedTextBox()
        {
            PreviewTextInput += MaskedTextBox_PreviewTextInput;
            PreviewKeyDown += MaskedTextBox_PreviewKeyDown;
            DataObject.AddPastingHandler(this, MaskedTextBox_Paste);
            LostFocus += MaskedTextBox_LostFocus;

            // Important: Set the initial Text to an empty string to avoid binding issues.
            Text = string.Empty;
        }


        private static void OnMaskChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MaskedTextBox mtb = (MaskedTextBox)d;
            mtb.ApplyMask();
        }

        private void MaskedTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            HandleTextInput(e.Text);
            e.Handled = true; // Prevent default text input.
        }

        private void MaskedTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                HandleBackspace();
                e.Handled = true;
            }
            else if (e.Key == Key.Delete)
            {
                HandleDelete();
                e.Handled = true;
            }
            else if (e.Key == Key.Space)
            {
                HandleTextInput(" ");  //Handle space like a regular char.
                e.Handled = true;
            }
            // Allow Ctrl+A (Select All)
            else if (e.Key == Key.A && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                SelectAll();
                e.Handled = true; // Still handle it even though we allow it
            }
            // Left/Right/Home/End
            else if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Home || e.Key == Key.End)
            {
                return; // Don't handle the key, let the TextBox handle the movement
            }
            else
            {
                e.Handled = false;  // Let other keys through (e.g. Shift for capitalization)
            }

        }

        private void MaskedTextBox_Paste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                HandleTextInput(pastedText);
            }
            e.CancelCommand(); // Prevent default paste behavior.
            e.Handled = true;
        }

        private void MaskedTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ApplyMask(); // Re-apply the mask when focus is lost
        }

        private void HandleTextInput(string input)
        {
            int insertPosition = SelectionStart;

            foreach (char c in input)
            {
                if (insertPosition >= Mask.Length)
                {
                    break; // Stop if we've reached the end of the mask.
                }

                if (Mask[insertPosition] == '0') // Numeric digit.
                {
                    if (char.IsDigit(c))
                    {
                        ReplaceCharInText(insertPosition, c);
                        insertPosition++;
                    }
                    // Skip non-digit characters.
                }
                else if (Mask[insertPosition] == 'L') // Alphabetic character.
                {
                    if (char.IsLetter(c))
                    {
                        ReplaceCharInText(insertPosition, c);
                        insertPosition++;
                    }
                    // Skip non-letter characters.
                }
                else if (Mask[insertPosition] == 'A')  //Alphanumeric
                {
                    if (char.IsLetterOrDigit(c))
                    {
                        ReplaceCharInText(insertPosition, c);
                        insertPosition++;
                    }
                }
                else // Literal character in the mask.
                {
                    if (Mask[insertPosition] == c)
                    {
                        //Allow direct entering the literal.
                        ReplaceCharInText(insertPosition, c);
                        insertPosition++;
                    }
                    else
                    {
                        // We are on a constant char, advance to the next editable position.
                        while (insertPosition < Mask.Length && Mask[insertPosition] != '0' && Mask[insertPosition] != 'L' && Mask[insertPosition] != 'A')
                        {
                            insertPosition++;
                        }

                        // Retry adding current char.
                        if (insertPosition < Mask.Length)
                        {
                            if ((Mask[insertPosition] == '0' && char.IsDigit(c)) ||
                               (Mask[insertPosition] == 'L' && char.IsLetter(c)) ||
                               (Mask[insertPosition] == 'A' && char.IsLetterOrDigit(c)))
                            {
                                ReplaceCharInText(insertPosition, c);
                                insertPosition++;
                            }
                        }
                    }
                }
            }
            SelectionStart = insertPosition;
        }


        private void ReplaceCharInText(int position, char newChar)
        {
            char[] textChars = Text.ToCharArray();
            if (position < textChars.Length)
            {
                textChars[position] = newChar;
            }
            else // Handle the case where the text is shorter than the mask.
            {
                //Add any missing chars from the mask up to the position we want to insert.
                string padding = "";
                for (int i = Text.Length; i < position; i++)
                {
                    padding += (Mask[i] == '0' || Mask[i] == 'L' || Mask[i] == 'A') ? MaskChar : Mask[i];
                }
                Text += padding;

                //Add new char.
                textChars = (Text + newChar).ToCharArray();
            }
            Text = new string(textChars);
        }



        private void HandleBackspace()
        {
            if (SelectionLength > 0)
            {
                // If there's a selection, delete the selection as usual
                HandleDeletion(SelectionStart, SelectionLength);
            }
            else if (SelectionStart > 0)
            {
                int prevPos = SelectionStart - 1;

                // Find the previous editable position.
                while (prevPos >= 0 && Mask[prevPos] != '0' && Mask[prevPos] != 'L' && Mask[prevPos] != 'A')
                {
                    prevPos--;
                }

                if (prevPos >= 0)
                {
                    HandleDeletion(prevPos, 1);
                }
            }
        }


        private void HandleDelete()
        {
            if (SelectionLength > 0)
            {
                // If there's a selection, delete the selection as usual.
                HandleDeletion(SelectionStart, SelectionLength);
            }
            else if (SelectionStart < Text.Length)
            {
                int nextPos = SelectionStart;

                // Find the next editable position.
                while (nextPos < Mask.Length && Mask[nextPos] != '0' && Mask[nextPos] != 'L' && Mask[nextPos] != 'A')
                {
                    nextPos++;
                }

                if (nextPos < Mask.Length)
                {
                    HandleDeletion(nextPos, 1);
                }
            }
        }


        private void HandleDeletion(int start, int length)
        {
            char[] textChars = Text.ToCharArray();
            for (int i = 0; i < length; i++)
            {
                if (start + i < textChars.Length)
                {
                    //Put an empty space, or if it is a fixed char, re-put the fixed char.
                    textChars[start + i] = (Mask[start + i] == '0' || Mask[start + i] == 'L' || Mask[start + i] == 'A') ? MaskChar : Mask[start + i];
                }
            }
            Text = new string(textChars);
            SelectionStart = start;  // Move cursor to the start of the deleted section.
        }


        private void ApplyMask()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                return; // No mask to apply.
            }

            char[] maskedText = new char[Mask.Length];
            int textIndex = 0;

            for (int i = 0; i < Mask.Length; i++)
            {
                if (Mask[i] == '0' || Mask[i] == 'L' || Mask[i] == 'A') // Placeholder.
                {
                    if (textIndex < Text.Length)
                    {
                        if ((Mask[i] == '0' && char.IsDigit(Text[textIndex])) ||
                            (Mask[i] == 'L' && char.IsLetter(Text[textIndex])) ||
                            (Mask[i] == 'A' && char.IsLetterOrDigit(Text[textIndex])))

                        {
                            maskedText[i] = Text[textIndex];
                            textIndex++;
                        }
                        else
                        {
                            // Invalid character, try to find the next valid one
                            while (textIndex < Text.Length &&
                                   !((Mask[i] == '0' && char.IsDigit(Text[textIndex])) ||
                                     (Mask[i] == 'L' && char.IsLetter(Text[textIndex])) ||
                                      (Mask[i] == 'A' && char.IsLetterOrDigit(Text[textIndex]))))
                            {
                                textIndex++;
                            }

                            if (textIndex < Text.Length)
                            {
                                maskedText[i] = Text[textIndex];
                                textIndex++;
                            }
                            else
                            {
                                maskedText[i] = MaskChar; // Fill with space if no valid char is found.
                            }
                        }
                    }
                    else
                    {
                        maskedText[i] = MaskChar; // Fill remaining placeholders with spaces.
                    }
                }
                else // Literal.
                {
                    maskedText[i] = Mask[i];
                    //if current char in text is equal to fixed mask char, move index forward.
                    if (textIndex < Text.Length && Text[textIndex] == Mask[i])
                    {
                        textIndex++;
                    }
                }
            }

            // Use Dispatcher.BeginInvoke to avoid reentrancy issues with TextChanged
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Text = new string(maskedText);
            }));
        }
    }
}