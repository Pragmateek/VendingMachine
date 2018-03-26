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
#### Cash register
##### Change algorithm
###### General principle
The change calculation algorithm is naive: it will explore the possible coins "chains" starting by the higher valued ones.
It will first try to get the maximum number of highest value coins, then reduce their number to give a chance to lower values coins to make the change.
###### Example
Say we have:
- n coins of 1$,
- n coins of 50c,
- n coins of 20c.

If we need to change 2.10$:
- the algorithm will first try to take 2 coins of 1$ looking for the remaining 10c in lower valued coins, but as there is no 10c or 5c coins it won't make a match,
- then it tries with only 1 coin of 1$, but 2 coins of 50c, the maximum it can get, but again it won't match as there is no 10c or 5c coins,
- so keeping the 1$ coin, it will then try with only 1 coin of 50c, then looking lower to get the change on the remaining 60c, and it will find it by taking 3 coins of 20c.

So the final chain is : 1$(1) + 50c(1) + 20c(3).
###### Remarks
This may not be optimal as we'll prefer to keep the maximum variety of coins to face the more change.

As an example if we have only 50c and 20c coins, 15n of each, and we request change on 1.50$ with the algorithm we'll get only 5n changes.  
Indeed each time we request 1.50$ we'll get 3 coins of 50c, without touching the 20c coins, and once the 50c coins are exhausted we can't make 1.50$ with 20c coins.  
But if we instead make 1.50$ with 5 coins of 20c and one of 50c we'll get 3n changes before exhausting the 20c coins, remains 12n 50c coins with which we can make another 4n changes, so a total of 7n changes, using all the coins.
## Data management
All the data related features like database persistence are in the VendingMachine.Data project
### Data repositories
## UI implementation
### MVVM pattern
#### Rationales
**MVVM** is yet another variation on the **MV*** paradigm.
The problematic is always the same: on one side you have a **model**, on the other side you have a **view**, and you want to avoid coupling them.
So you add a third layer between them, and you call it a *controller*, a *presenter*, a *view-model*...
The **view-model** (**VM**) keeps the **application logic**, letting the view only managing visual rendering of the business entities.
#### Implementation
In WinForms we can leverage **data-binding** to create a "real-time" link between the data exposed by the VM and their display by the control.
It's why VMs and some business entities implements the ***INotifyPropertyChanged*** interface which is a contract which ensures the view it will be notified when the entity changes.