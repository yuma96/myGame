<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\Ranktable $ranktable
 */
?>
<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Html->link(__('List Ranktable'), ['action' => 'index']) ?></li>
    </ul>
</nav>
<div class="ranktable form large-9 medium-8 columns content">
    <?= $this->Form->create($ranktable) ?>
    <fieldset>
        <legend><?= __('Add Ranktable') ?></legend>
        <?php
            echo $this->Form->control('Name');
            echo $this->Form->control('Time');
        ?>
    </fieldset>
    <?= $this->Form->button(__('Submit')) ?>
    <?= $this->Form->end() ?>
</div>
