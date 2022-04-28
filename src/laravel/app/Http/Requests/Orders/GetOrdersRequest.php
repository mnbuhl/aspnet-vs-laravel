<?php

namespace App\Http\Requests\Orders;

use Illuminate\Foundation\Http\FormRequest;

class GetOrdersRequest extends FormRequest
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
            'pageIndex' => 'sometimes|integer|min:1',
            'pageSize' => 'sometimes|integer|min:1|max::50',
            'sort' => 'sometimes|string|in:total,-total,date,-date',
            'userId' => 'sometimes|uuid'
        ];
    }
}
