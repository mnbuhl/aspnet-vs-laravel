<?php

namespace App\Models;

use App\Traits\HasUniqueIdentifier;
use Eloquent;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Carbon;

/**
 * App\Models\Address
 *
 * @property string $uuid
 * @property string $address_line
 * @property string $city
 * @property string $zip_code
 * @property string $country
 * @property Carbon|null $created_at
 * @property Carbon|null $updated_at
 * @method static Builder|Address newModelQuery()
 * @method static Builder|Address newQuery()
 * @method static Builder|Address query()
 * @method static Builder|Address whereAddressLine($value)
 * @method static Builder|Address whereCity($value)
 * @method static Builder|Address whereCountry($value)
 * @method static Builder|Address whereCreatedAt($value)
 * @method static Builder|Address whereUpdatedAt($value)
 * @method static Builder|Address whereUuid($value)
 * @method static Builder|Address whereZipCode($value)
 * @mixin Eloquent
 */
class Address extends Model
{
    use HasFactory, HasUniqueIdentifier;
}
