<?php

namespace App\Models;

use App\Traits\HasUniqueIdentifier;
use Eloquent;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
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
 */
class OrderLine extends Model
{
    use HasFactory, HasUniqueIdentifier;
}
