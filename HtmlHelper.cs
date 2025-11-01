using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using Newtonsoft.Json;


namespace p2
{
    public class HtmlHelper
    {
        public string[] ContainerTags { get; private set; }
        public string[] SelfClosingTags { get; private set; }
        private readonly static HtmlHelper instance = new HtmlHelper();
        public static HtmlHelper Instance
        {
            get
            {
                return instance;
            }
        }
        //FILE


        private HtmlHelper()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var allPath = Path.Combine(baseDir, "JSON Files", "HtmlTags.json");
            var voidPath = Path.Combine(baseDir, "JSON Files", "HtmlVoidTags.json");
            LoadTags(allPath, voidPath);
        }



        /*
         
        C:\Users\USER\Desktop\יונית\שנה ב\פרקטיקוד\JSON Files



         הקוד שסיפקת מגדיר מחלקת HtmlHelper בשפת C#, שמטרתה לטעון רשימות של תגיות HTML מקבצי JSON. הנה הסבר מפורט על כל חלק בקוד:

1. using Statements
using System;
using System.IO;
using Newtonsoft.Json;
System: כולל סוגים בסיסיים כמו String, Int32 וכו'.
System.IO: מספק גישה לפונקציות של קלט/פלט, כמו קריאה וכתיבה לקבצים.
Newtonsoft.Json: ספריה פופולרית לפרש נתוני JSON.
2. הגדרת מחלקת HtmlHelper
public class HtmlHelper
{
    public string[] AllTags { get; private set; }
    public string[] SelfClosingTags { get; private set; }
מחלקה זו מכילה שני מאפיינים:
AllTags: מערך של מחרוזות המייצג את כל התגיות ב-HTML.
SelfClosingTags: מערך של מחרוזות המייצג את התגיות שאינן דורשות סגירה.
3. הקונסטרקטור
public HtmlHelper(string allTagsFilePath, string selfClosingTagsFilePath)
{
    LoadTags(allTagsFilePath, selfClosingTagsFilePath);
}
הקונסטרקטור מקבל שני נתיבים לקבצים (מחרוזות) ומבצע קריאה לפונקציה LoadTags כדי לטעון את הנתונים.
4. פונקציית LoadTags
private void LoadTags(string allTagsFilePath, string selfClosingTagsFilePath)
{
    if (File.Exists(allTagsFilePath))
    {
        var allTagsJson = File.ReadAllText(allTagsFilePath);
        AllTags = JsonConvert.DeserializeObject<string[]>(allTagsJson);
    }

    if (File.Exists(selfClosingTagsFilePath))
    {
        var selfClosingTagsJson = File.ReadAllText(selfClosingTagsFilePath);
        SelfClosingTags = JsonConvert.DeserializeObject<string[]>(selfClosingTagsJson);
    }
}
הפונקציה בודקת אם הקבצים קיימים בנתיב שניתן:
אם קובץ allTagsFilePath קיים, היא קוראת את תוכנו ומפרשת אותו כ-JSON למערך של מחרוזות בעזרת JsonConvert.DeserializeObject.
אם קובץ selfClosingTagsFilePath קיים, היא עושה את אותו התהליך עבור התגיות שאינן דורשות סגירה.
סיכום
מחלקת HtmlHelper מספקת דרך נוחה לטעון ולנהל רשימות של תגיות HTML מקבצי JSON. חשוב לוודא שהקבצים קיימים בנתיב שניתן וכי הם בפורמט הנכון כדי שהקוד יעבוד כראוי.

בשלב השלישי לא הבנתי מה זה loadtags

LoadTags היא פונקציה פרטית במחלקת HtmlHelper, שתפקידה לטעון את הנתונים מקבצי JSON לתוך המאפיינים של המחלקה.

הסבר מפורט על LoadTags:
פרמטרים: הפונקציה מקבלת שני פרמטרים:

allTagsFilePath: נתיב לקובץ JSON שמכיל את כל התגיות.
selfClosingTagsFilePath: נתיב לקובץ JSON שמכיל את התגיות שאינן דורשות סגירה.
בדיקת קיום קובץ:

הפונקציה בודקת אם הקובץ allTagsFilePath קיים בעזרת File.Exists.
אם הקובץ קיים, היא קוראת את תוכנו בעזרת File.ReadAllText ומאחסנת אותו במשתנה allTagsJson.
פירוש JSON:

לאחר קריאת התוכן, הפונקציה משתמשת ב-JsonConvert.DeserializeObject<string[]>(allTagsJson) כדי לפרש את המידע מ-JSON למערך של מחרוזות (string[]).
המערך המתקבל מאוחסן במאפיין AllTags.
חזרה על התהליך:

הפונקציה חוזרת על אותו תהליך עבור קובץ selfClosingTagsFilePath, אם הוא קיים.
סיכום
LoadTags היא פונקציה מרכזית במחלקה, שאחראית על טעינת הנתונים מקבצים חיצוניים והכנתם לשימוש במאפיינים של המחלקה.
         
         
         
         */




        private void LoadTags(string allTagsFilePath, string selfClosingTagsFilePath)
        {
            if (File.Exists(allTagsFilePath))
            {
                var ContainerTagsJson = File.ReadAllText(allTagsFilePath);
                ContainerTags = JsonConvert.DeserializeObject<string[]>(ContainerTagsJson);
            }
            else
            {
                ContainerTags = Array.Empty<string>();
            }


            if (File.Exists(selfClosingTagsFilePath))
            {
                var selfClosingTagsJson = File.ReadAllText(selfClosingTagsFilePath);
                SelfClosingTags = JsonConvert.DeserializeObject<string[]>(selfClosingTagsJson);
            }
            else
            {
                SelfClosingTags = Array.Empty<string>();
            }
        }
    }
}
