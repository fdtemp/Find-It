using UnityEngine;

public class Script_UI : MonoBehaviour {
    public System.Action mouseDown = null;
    public System.Action mouseEnter = null;
    public System.Action mouseExit = null;
    public System.Action mouseOver = null;
    public System.Action mouseUp = null;

    public void OnMouseDown() { if (mouseDown != null) mouseDown(); }
    public void OnMouseEnter() { if (mouseEnter != null) mouseEnter(); }
    public void OnMouseExit() { if (mouseExit != null) mouseExit(); }
    public void OnMouseOver() { if (mouseOver != null) mouseOver(); }
    public void OnMouseUp() { if (mouseUp != null) mouseUp(); }
}