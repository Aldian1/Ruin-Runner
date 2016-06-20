using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;

public class BugReporter : MonoBehaviour
{

    public GameObject textobj;
    public GameObject email_;

    public void SendEmail()
    {
        String BODY = textobj.GetComponent<InputField>().text;
        String subjectmatter = email_.GetComponent<Text>().text;

        string email = "harry@imaginaryspace.co.uk";
        string subject = MyEscapeURL("Bug Report from: " + subjectmatter);
        string body = MyEscapeURL(BODY);
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
}