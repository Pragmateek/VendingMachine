# Vending Machine
## General architecture
The project items have been split amongst many projects, each handling a specific problematic:
- business entities contracts,
- business entities implementations,
- data management,
- UI controls,
- UI client,
- tests,
- tooling.
## Business model
### Contracts
The business entities contracts are in the VendingMachine.Business.Contracts project.
#### Products and items dichotomy
A product is a general representation of a set of identical items, like a class represents a set of instances.
A product would be the BK Whopper as a general kind of burger, and an item would be the Whopper you have in your box ready to be eaten.
Each product is represented by a singleton instance of the Product class.
#### Coin-types and coins dichotomy
This is similar to the product/item dichotomy: a coin-type may be the "half dollar" as a general type of coin, and a half dollar coin would be the one you use to buy your Whopper.
Each coin-type is represented by a singleton instance of the CoinType class.
### Implementations
The business entities implementations are in the VendingMachine.Business.Implementation project.
#### Store slot
This implementation uses a queue internally to mimic the way items would be fed and consumed into a vending-machine: stacked one above the previous, with the bottom one being the first to be bought.
This is consistent with the graphical representation of the store.
In other kind of vending machine we would get the reverse if items are store horizontally: the first to be added is in the back, and you get the one in the front first.
## Data management
All the data related features like database persistence are in the VendingMachine.Data project
### Data repositories
## UI implementation
### MVVM pattern
#### Rationales
MVVM is yet another variation on the MV* paradigm.
The problematic is always the same: one one side you have a model, on the other side you have a view, and you want to avoid coupling them.
So you add a third layer between them, and you call it a controller, a presenter, a view-model...
#### Implementation
