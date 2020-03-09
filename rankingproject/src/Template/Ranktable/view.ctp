<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\Ranktable $ranktable
 */
?>
<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Html->link(__('Edit Ranktable'), ['action' => 'edit', $ranktable->Id]) ?> </li>
        <li><?= $this->Form->postLink(__('Delete Ranktable'), ['action' => 'delete', $ranktable->Id], ['confirm' => __('Are you sure you want to delete # {0}?', $ranktable->Id)]) ?> </li>
        <li><?= $this->Html->link(__('List Ranktable'), ['action' => 'index']) ?> </li>
        <li><?= $this->Html->link(__('New Ranktable'), ['action' => 'add']) ?> </li>
    </ul>
</nav>
<div class="ranktable view large-9 medium-8 columns content">
    <h3><?= h($ranktable->Id) ?></h3>
    <table class="vertical-table">
        <tr>
            <th scope="row"><?= __('Name') ?></th>
            <td><?= h($ranktable->Name) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Id') ?></th>
            <td><?= $this->Number->format($ranktable->Id) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Time') ?></th>
            <td><?= $this->Number->format($ranktable->Time) ?></td>
        </tr>
    </table>
</div>
