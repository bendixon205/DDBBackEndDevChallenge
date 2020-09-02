# DnDBeyond Back-End Dev Challenge

Submitted by Ben Dixon

## Features

### Retrieve the Character

Retrieves a JSON representation of the character.

Endpoint: `http://{serviceUri}/`

Parameters:

- None

Output:

- JSON representation of character

```json
{
    "name": "Briv",
    "level": 5,
    "currentHitPoints": 41,
    "maxHitPoints": 41,
    "currentTemporaryHitPoints": 0,
    "classes": [
        {
            "name": "fighter",
            "hitDiceValue": 10,
            "classLevel": 3
        },
        {
            "name": "wizard",
            "hitDiceValue": 6,
            "classLevel": 2
        }
    ],
    "stats": {
        "strength": 15,
        "dexterity": 12,
        "constitution": 16,
        "intelligence": 13,
        "wisdom": 10,
        "charisma": 8
    },
    "items": [
        {
            "name": "Ioun Stone of Fortitude",
            "modifier": {
                "affectedObject": "stats",
                "affectedValue": "constitution",
                "value": 2
            }
        }
    ],
    "defenses": [
        {
            "type": "fire",
            "defense": "immunity"
        },
        {
            "type": "slashing",
            "defense": "resistance"
        },
        {
            "type": "cold",
            "defense": "vulnerability"
        }
    ]
}
```

### Heal

Recover a specified amount of hit points.

Endpoint: `http://{serviceUri}/heal`

Parameters:

- Amount to be healed

```json
{
    "amount" : 10
}
```

Output:

- JSON representation of character

```json
{
    "name": "Briv",
    "level": 5,
    "currentHitPoints": 41,
    "maxHitPoints": 41,
    "currentTemporaryHitPoints": 0,
    "classes": [
        {
            "name": "fighter",
            "hitDiceValue": 10,
            "classLevel": 3
        },
        {
            "name": "wizard",
            "hitDiceValue": 6,
            "classLevel": 2
        }
    ],
    "stats": {
        "strength": 15,
        "dexterity": 12,
        "constitution": 16,
        "intelligence": 13,
        "wisdom": 10,
        "charisma": 8
    },
    "items": [
        {
            "name": "Ioun Stone of Fortitude",
            "modifier": {
                "affectedObject": "stats",
                "affectedValue": "constitution",
                "value": 2
            }
        }
    ],
    "defenses": [
        {
            "type": "fire",
            "defense": "immunity"
        },
        {
            "type": "slashing",
            "defense": "resistance"
        },
        {
            "type": "cold",
            "defense": "vulnerability"
        }
    ]
}
```

### Add Temporary HP

Attempts to add temporary HP to the character, taking the higher value.

Endpoint: `http://{serviceUri}/addTempHP`

Parameters:

- Amount of temporary HP to attempt to add

```json
{
    "amount" : 10
}
```

Output:

- JSON representation of character

```json
{
    "name": "Briv",
    "level": 5,
    "currentHitPoints": 41,
    "maxHitPoints": 41,
    "currentTemporaryHitPoints": 10,
    "classes": [
        {
            "name": "fighter",
            "hitDiceValue": 10,
            "classLevel": 3
        },
        {
            "name": "wizard",
            "hitDiceValue": 6,
            "classLevel": 2
        }
    ],
    "stats": {
        "strength": 15,
        "dexterity": 12,
        "constitution": 16,
        "intelligence": 13,
        "wisdom": 10,
        "charisma": 8
    },
    "items": [
        {
            "name": "Ioun Stone of Fortitude",
            "modifier": {
                "affectedObject": "stats",
                "affectedValue": "constitution",
                "value": 2
            }
        }
    ],
    "defenses": [
        {
            "type": "fire",
            "defense": "immunity"
        },
        {
            "type": "slashing",
            "defense": "resistance"
        },
        {
            "type": "cold",
            "defense": "vulnerability"
        }
    ]
}
```

### Deal Damage

Deals damage to the character of the provided type, respecting defenses and vulnerabilities.

Endpoint: `http://{serviceUri}/dealDamage`

Parameters:

- Amount of damage
- Type of damage

```json
{
    "amount" : 15,
    "damageType": "piercing"
}
```

Output:

- JSON representation of character

```json
{
    "name": "Briv",
    "level": 5,
    "currentHitPoints": 26,
    "maxHitPoints": 41,
    "currentTemporaryHitPoints": 0,
    "classes": [
        {
            "name": "fighter",
            "hitDiceValue": 10,
            "classLevel": 3
        },
        {
            "name": "wizard",
            "hitDiceValue": 6,
            "classLevel": 2
        }
    ],
    "stats": {
        "strength": 15,
        "dexterity": 12,
        "constitution": 16,
        "intelligence": 13,
        "wisdom": 10,
        "charisma": 8
    },
    "items": [
        {
            "name": "Ioun Stone of Fortitude",
            "modifier": {
                "affectedObject": "stats",
                "affectedValue": "constitution",
                "value": 2
            }
        }
    ],
    "defenses": [
        {
            "type": "fire",
            "defense": "immunity"
        },
        {
            "type": "slashing",
            "defense": "resistance"
        },
        {
            "type": "cold",
            "defense": "vulnerability"
        }
    ]
}
```
