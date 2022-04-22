<?php

namespace App\Models;

use App\Traits\HasUniqueIdentifier;
use Eloquent;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
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
 */
class ShippingDetails extends Model
{
    use HasFactory, HasUniqueIdentifier;
}
