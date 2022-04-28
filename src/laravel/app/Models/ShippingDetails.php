<?php

namespace App\Models;

use App\Traits\HasUniqueIdentifier;
use Database\Factories\ShippingDetailsFactory;
use Eloquent;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Support\Carbon;

/**
 * App\Models\ShippingDetails
 *
 * @property string $id
 * @property string|null $shipped_at
 * @property string|null $delivered_at
 * @property string $shipping_address_id
 * @property Carbon|null $created_at
 * @property Carbon|null $updated_at
 * @property-read Address $shippingAddress
 * @method static Builder|ShippingDetails newModelQuery()
 * @method static Builder|ShippingDetails newQuery()
 * @method static Builder|ShippingDetails query()
 * @method static Builder|ShippingDetails whereCreatedAt($value)
 * @method static Builder|ShippingDetails whereDeliveredAt($value)
 * @method static Builder|ShippingDetails whereId($value)
 * @method static Builder|ShippingDetails whereShippedAt($value)
 * @method static Builder|ShippingDetails whereShippingAddressId($value)
 * @method static Builder|ShippingDetails whereUpdatedAt($value)
 * @mixin Eloquent
 * @method static ShippingDetailsFactory factory(...$parameters)
 */
class ShippingDetails extends Model
{
    use HasFactory, HasUniqueIdentifier;

    protected $fillable = [
        'shipped_at',
        'delivered_at',
        'shipping_address_id',
    ];

    public function shippingAddress(): BelongsTo
    {
        return $this->belongsTo(Address::class);
    }
}
