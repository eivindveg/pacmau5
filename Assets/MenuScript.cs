using UnityEngine;

// ReSharper disable once CheckNamespace
public class MenuScript : MonoBehaviour
{
    private const float Speed = 0.01f;
    private float a = 1f;
    private bool last;
    private bool stop;
	public static bool startAnim = false;

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private void OnGUI()
    {
        if (Logo.showmenu && !this.stop)
        {
            this.a -= Speed;
            var color = new Color(this.a, this.a, this.a);
            Camera.main.backgroundColor = color;
            RenderSettings.ambientLight = color;

            if (this.a < 0)
            {
                this.last = true;
            }
        }

        if (this.last && !this.stop)
        {
			startAnim = true;
			var go = (GameObject)Instantiate(Resources.Load("menubg"));
            go.transform.position = new Vector3(0, 0, 0);
            go.guiTexture.pixelInset = new Rect ((Screen.width / 2) - (Screen.width / 2) / 2, (Screen.height / 2) - (Screen.height / 2) / 2, (Screen.width / 2) ,(Screen.height / 2));
            this.stop = true;
            Logo.showmenu = false;
        }

        if (this.stop)
        {
            if (GUI.Button(new Rect((Screen.width / 2) - 180, (Screen.height / 2) + 120, 120, 30), "Start"))
            {
                Application.LoadLevel(1);
            }

            if (GUI.Button(new Rect((Screen.width / 2) + 50, (Screen.height / 2) + 120, 120, 30), "Quit"))
            {
                Application.Quit();
            }
        }
    }
}
