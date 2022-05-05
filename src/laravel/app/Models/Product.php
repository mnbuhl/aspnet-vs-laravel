<?php

namespace App\Models;

use App\Traits\HasUniqueIdentifier;
use Database\Factories\ProductFactory;
use Eloquent;
use Exception;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Carbon;

/**
 * App\Models\Product
 *
 * @property string $id
 * @property string $name
 * @property int $price
 * @property string $description
 * @property int $amount_in_stock
 * @property Carbon|null $created_at
 * @property Carbon|null $updated_at
 * @method static ProductFactory factory(...$parameters)
 * @method static Builder|Product newModelQuery()
 * @method static Builder|Product newQuery()
 * @method static Builder|Product query()
 * @method static Builder|Product whereAmountInStock($value)
 * @method static Builder|Product whereCreatedAt($value)
 * @method static Builder|Product whereDescription($value)
 * @method static Builder|Product whereId($value)
 * @method static Builder|Product whereName($value)
 * @method static Builder|Product wherePrice($value)
 * @method static Builder|Product whereUpdatedAt($value)
 * @mixin Eloquent
 */
class Product extends Model
{
    use HasFactory, HasUniqueIdentifier;

    protected $fillable = [
        'id',
        'name',
        'description',
        'price',
        'amount_in_stock'
    ];

    /**
     * @throws Exception
     */
    public function updateQuantity(int $quantity): Product
    {
        if ($this->amount_in_stock < 0) {
            throw new Exception('Product is out of stock.', 400);
        }

        $this->amount_in_stock -= $quantity;
        return $this;
    }
}
