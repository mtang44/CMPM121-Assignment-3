using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
public class Effect{
    
    public string description;
    public string type;
    public string amount;
    public string until;
    public Effect()
    {

    }
    void Start(){       
    }

        // Update is called once per frame
    void Update(){
    }
     public Effect setAttributes(JObject attributes)
    {
        this.description = attributes["effect"]["description"].ToString();
        this.type = attributes["effect"]["type"].ToString();
        this.amount = attributes["effect"]["amount"].ToString();
        this.until = attributes["effect"]["until"].ToString();
        return this;

    }
}