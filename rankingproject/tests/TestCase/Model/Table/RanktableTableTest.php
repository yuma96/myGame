<?php
namespace App\Test\TestCase\Model\Table;

use App\Model\Table\RanktableTable;
use Cake\ORM\TableRegistry;
use Cake\TestSuite\TestCase;

/**
 * App\Model\Table\RanktableTable Test Case
 */
class RanktableTableTest extends TestCase
{

    /**
     * Test subject
     *
     * @var \App\Model\Table\RanktableTable
     */
    public $Ranktable;

    /**
     * Fixtures
     *
     * @var array
     */
    public $fixtures = [
        'app.ranktable'
    ];

    /**
     * setUp method
     *
     * @return void
     */
    public function setUp()
    {
        parent::setUp();
        $config = TableRegistry::getTableLocator()->exists('Ranktable') ? [] : ['className' => RanktableTable::class];
        $this->Ranktable = TableRegistry::getTableLocator()->get('Ranktable', $config);
    }

    /**
     * tearDown method
     *
     * @return void
     */
    public function tearDown()
    {
        unset($this->Ranktable);

        parent::tearDown();
    }

    /**
     * Test initialize method
     *
     * @return void
     */
    public function testInitialize()
    {
        $this->markTestIncomplete('Not implemented yet.');
    }

    /**
     * Test validationDefault method
     *
     * @return void
     */
    public function testValidationDefault()
    {
        $this->markTestIncomplete('Not implemented yet.');
    }
}
