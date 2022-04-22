<?php

namespace App\Models;

use App\Traits\HasUniqueIdentifier;
use Database\Factories\OrderFactory;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Carbon;

/**
 * App\Models\Order
 *
 * @property string $id
 * @property int $total
 * @property string $date
 * @property string $user_id
 * @property string $billing_address_id
 * @property string $shipping_details_id
 * @property Carbon|null $created_at
 * @property Carbon|null $updated_at
 * @method static OrderFactory factory(...$parameters)
 * @method static Builder|Order newModelQuery()
 * @method static Builder|Order newQuery()
 * @method static Builder|Order query()
 * @method static Builder|Order whereBillingAddressId($value)
 * @method static Builder|Order whereCreatedAt($value)
 * @method static Builder|Order whereDate($value)
 * @method static Builder|Order whereId($value)
 * @method static Builder|Order whereShippingDetailsId($value)
 * @method static Builder|Order whereTotal($value)
 * @method static Builder|Order whereUpdatedAt($value)
 * @method static Builder|Order whereUserId($value)
 * @mixin \Eloquent
 */
class Order extends Model
{
    use HasFactory, HasUniqueIdentifier;
}
