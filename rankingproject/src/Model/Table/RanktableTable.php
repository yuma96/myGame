<?php
namespace App\Model\Table;

use Cake\ORM\Query;
use Cake\ORM\RulesChecker;
use Cake\ORM\Table;
use Cake\Validation\Validator;

/**
 * Ranktable Model
 *
 * @method \App\Model\Entity\Ranktable get($primaryKey, $options = [])
 * @method \App\Model\Entity\Ranktable newEntity($data = null, array $options = [])
 * @method \App\Model\Entity\Ranktable[] newEntities(array $data, array $options = [])
 * @method \App\Model\Entity\Ranktable|bool save(\Cake\Datasource\EntityInterface $entity, $options = [])
 * @method \App\Model\Entity\Ranktable|bool saveOrFail(\Cake\Datasource\EntityInterface $entity, $options = [])
 * @method \App\Model\Entity\Ranktable patchEntity(\Cake\Datasource\EntityInterface $entity, array $data, array $options = [])
 * @method \App\Model\Entity\Ranktable[] patchEntities($entities, array $data, array $options = [])
 * @method \App\Model\Entity\Ranktable findOrCreate($search, callable $callback = null, $options = [])
 */
class RanktableTable extends Table
{

    /**
     * Initialize method
     *
     * @param array $config The configuration for the Table.
     * @return void
     */
    public function initialize(array $config)
    {
        parent::initialize($config);

        $this->setTable('ranktable');
        $this->setDisplayField('Id');
        $this->setPrimaryKey('Id');
    }

    /**
     * Default validation rules.
     *
     * @param \Cake\Validation\Validator $validator Validator instance.
     * @return \Cake\Validation\Validator
     */
    public function validationDefault(Validator $validator)
    {
        $validator
            ->integer('Id')
            ->allowEmpty('Id', 'create');

        $validator
            ->scalar('Name')
            ->maxLength('Name', 30)
            ->requirePresence('Name', 'create')
            ->notEmpty('Name');

        $validator
            ->integer('Time')
            ->requirePresence('Time', 'create')
            ->notEmpty('Time');

        return $validator;
    }
}
