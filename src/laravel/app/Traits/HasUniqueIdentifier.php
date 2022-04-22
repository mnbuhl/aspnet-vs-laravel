<?php

namespace App\Traits;

trait HasUniqueIdentifier
{
    public static function boot(): void
    {
        parent::boot();
        static::withCasts(['id' => 'string']);
        static::creating(function (Model $model) {
            $model->setKeyType('string');
            $model->setIncrementing(false);
            $model->setAttribute($model->getKeyName(), Uuid::uuid4());
        });
    }

    public function getIncrementing(): bool
    {
        return false;
    }

    public function getKeyType(): string
    {
        return 'string';
    }
}
