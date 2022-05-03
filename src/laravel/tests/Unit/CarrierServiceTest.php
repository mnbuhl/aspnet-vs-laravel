<?php

namespace Tests\Unit;

use App\Services\Interfaces\ICarrierService;
use Ramsey\Uuid\Uuid;
use Tests\TestCase;

class CarrierServiceTest extends TestCase
{
    private ICarrierService $carrierService;

    public function setUp(): void
    {
        parent::setUp();
        $this->carrierService = $this->app->make(ICarrierService::class);
    }

    /** @test */
    public function carrier_service_generates_random_boolean_from_uuid(): void
    {
        $results = [];

        for ($i = 0; $i < 100; $i++) {
            $results[] = $this->carrierService->isOrderShipped(Uuid::uuid4());
            $results[] = $this->carrierService->isOrderDelivered(Uuid::uuid4());
        }

        foreach ($results as $result) {
            $this->assertTrue($result === true || $result === false);
        }
    }

    /** @test */
    public function carrier_service_generates_more_false_than_true(): void
    {
        $trueCount = 0;
        $falseCount = 0;

        for ($i = 0; $i < 100; $i++) {
            if ($this->carrierService->isOrderShipped(Uuid::uuid4())) {
                $trueCount++;
            } else {
                $falseCount++;
            }

            if ($this->carrierService->isOrderDelivered(Uuid::uuid4())) {
                $trueCount++;
            } else {
                $falseCount++;
            }
        }

        $this->assertTrue($trueCount < $falseCount);
    }
}
