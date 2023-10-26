using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEComment
    {
        public string CommentText { get; private set; }

        public EXEComment(string commentText)
        {
            this.CommentText = commentText;
        }
    }
}