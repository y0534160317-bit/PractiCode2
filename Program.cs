using System.Text.Json;
using System.Net.Http;
using p2;
//rajex

var htmllines=new Rajex("<{.*}>");




string str=await Load("https://learn.malkabruk.co.il/practicode/projects/pract-2/#_2");
HtmlElement root=new HtmlParser().ParseHtml(str);


Console.WriteLine(str);