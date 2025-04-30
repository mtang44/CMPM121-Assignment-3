public class ArcaneSpray : Spell {

    private string N;
    private string spray;
    public ArcaneSpray () {

    }

    public void override SetAttributes(JObject attributes) {
        base.SetAttributes();
        N = attributes["N"].ToString();
        spray = attributes["spray"].ToString();
    }
}