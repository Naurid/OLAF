namespace DisneyBattle.Interfaces;

public interface ICRUInterface<TEntity, TKey>
{
    bool Insert(TEntity entity);
    bool Update(TKey id, TEntity entity);
    TEntity GetById(TKey id);
    IEnumerable<TEntity> GetAll();
}