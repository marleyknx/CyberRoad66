using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float _speed = 8;
    public float Speed { get => _speed; set => _speed = value; }



    public void Move(float amount,Vector3 movementAxis)
    {
        if (amount == 0 ) return;

        
        
        transform.position += movementAxis * amount  * _speed * Time.deltaTime;

    }
}
