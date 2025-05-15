using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
public class Trigger{

    public string description;
    public string type;
    public string amount;


    public Trigger()
    {
        
    }
    public  Trigger setAttributes(JObject attributes)
    {
        this.description = attributes["trigger"]["description"].ToString();
        this.type = attributes["trigger"]["type"].ToString();
        this.amount = attributes["trigger"]["amount"].ToString();
        return this;

    }

    void Start()
        {
            
        }

        // Update is called once per frame
    void Update()
        {
            
        }





}