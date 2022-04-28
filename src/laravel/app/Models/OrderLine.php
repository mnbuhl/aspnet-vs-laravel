<?php

namespace App\Models;

use App\Traits\HasUniqueIdentifier;
use Database\Factories\OrderLineFactory;
use Eloquent;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Support\Carbon;

/**
 * App\Models\OrderLine
 *
 * @property string $id
 * @property int $price
 * @property int $quantity
 * @property int $discount
 * @property string $order_id
 * @property string $product_id
 * @property Carbon|null $created_at
 * @property Carbon|null $updated_at
 * @property-read Order $order
 * @property-read Product $product
 * @method static Builder|OrderLine newModelQuery()
 * @method static Builder|OrderLine newQuery()
 * @method static Builder|OrderLine query()
 * @method static Builder|OrderLine whereCreatedAt($value)
 * @method static Builder|OrderLine whereDiscount($value)
 * @method static Builder|OrderLine whereId($value)
 * @method static Builder|OrderLine whereOrderId($value)
 * @method static Builder|OrderLine wherePrice($value)
 * @method static Builder|OrderLine whereProductId($value)
 * @method static Builder|OrderLine whereQuantity($value)
 * @method static Builder|OrderLine whereUpdatedAt($value)
 * @mixin Eloquent
 * @method static OrderLineFactory factory(...$parameters)
 */
class OrderLine extends Model
{
    use HasFactory, HasUniqueIdentifier;

    protected $fillable = [
        'price',
        'quantity',
        'discount',
        'product_id',
    ];

    public function order(): BelongsTo
    {
        return $this->belongsTo(Order::class);
    }

    public function product(): BelongsTo
    {
        return $this->belongsTo(Product::class);
    }
}
