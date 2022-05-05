<?php

namespace App\Http\Requests\Orders;

use Illuminate\Foundation\Http\FormRequest;

class StoreOrderRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize(): bool
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules(): array
    {
        return [
            'id' => 'sometimes|uuid',
            'date' => 'required|date',
            'user_id' => 'required|exists:users,id',

            'order_lines.*.product_id' => 'required|exists:products,id',
            'order_lines.*.price' => 'required|numeric',
            'order_lines.*.quantity' => 'required|numeric:min:1',
            'order_lines.*.discount' => 'required|numeric:min:0:max:100',

            'shipping_details.shipped_at' => 'date|nullable',
            'shipping_details.delivered_at' => 'date|nullable|after:shippingDetails.shipped_at',
            'shipping_details.carrier' => 'required|string',
            'shipping_details.shipping_address_id' => 'nullable|exists:shipping_addresses,id',
            'shipping_details.shipping_address.address_line' => 'string|required_unless:shippingDetails.shipping_address_id,null',
            'shipping_details.shipping_address.city' => 'string|required_unless:shippingDetails.shipping_address_id,null',
            'shipping_details.shipping_address.country' => 'string|required_unless:shippingDetails.shipping_address_id,null',
            'shipping_details.shipping_address.zip_code' => 'string|required_unless:shippingDetails.shipping_address_id,null',

            'billing_address_id' => 'nullable|exists:billing_addresses,id',
            'billing_address' => 'array|nullable|required_unless:billing_address_id,null',
            'billing_address.address_line' => 'string|required_unless:billing_address_id,null',
            'billing_address.city' => 'string|required_unless:billing_address_id,null',
            'billing_address.country' => 'string|required_unless:billing_address_id,null',
            'billing_address.zip_code' => 'string|required_unless:billing_address_id,null',
        ];
    }
}
