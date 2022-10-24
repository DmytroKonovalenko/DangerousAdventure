public interface IDestrucable
{
    float Health { get; set; }
    void RecieveHit(float damage);
    void Die();
}