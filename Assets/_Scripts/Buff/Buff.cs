using UnityEngine;

public class Buff : MonoBehaviour
{
    private BuffConfig buffConfig;
    public BuffConfig BuffConfig
    { 
        get => buffConfig;
        set => buffConfig = value;
    }
    public virtual void ApplyBuff()
    {

    }
    public virtual void RemoveBuff()
    {

    }
}
