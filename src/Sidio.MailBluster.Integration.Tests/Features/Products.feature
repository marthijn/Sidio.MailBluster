Feature: CRUD operations for products

@cleanupProduct
Scenario: Create a product
    Given a product does not exist
    When the product is created
    Then the product should exist

@createProduct
@cleanupProduct
Scenario: Update a product
    Given a product exists
    When the product is updated
    Then the product should be updated

@createProduct
Scenario: Delete a product
    Given a product exists
    When the product is deleted
    Then the product should not exist