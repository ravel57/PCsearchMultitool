using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Drawing;
 
namespace pc_finnder {
    class ColoredCheckedListBox : CheckedListBox {
        public ColoredCheckedListBox() {
        }

        protected override void OnDrawItem(DrawItemEventArgs args) {
            if (args.Index < 0 || args.Index >= Items.Count) {
                base.OnDrawItem(args);
                return;
            }

            IColored colored = Items[args.Index] as IColored;
            Color foreColor = colored == null ? args.ForeColor : colored.ForeColor;
            var newArgs = new DrawItemEventArgs(
                args.Graphics, args.Font, args.Bounds,
                args.Index,
                args.State,
                foreColor,
                args.BackColor
            );

            base.OnDrawItem(newArgs);
        }
    }

    interface IColored {
        Color ForeColor { get; }
    }
}