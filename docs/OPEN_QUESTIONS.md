# raWWar — Open Questions

Status: Living elicitation register; unresolved by design.
Owner: raWWar creator / game design.
Audience: Creator, design collaborators, narrative and systems specialists, and implementation agents.
Evidence rule: A question is not a decision. Do not fill a gap with generic assumptions or treat a proposed answer as canon without creator confirmation.

This is deliberately a living interrogation list.

The purpose is to expose what the creator knows but has not yet articulated, not to fill the gaps with generic game design.

## Highest-priority questions

### 1. What is the war?

What conflict creates the world?

Who is fighting?

Why?

What does each side believe it is protecting or achieving?

### 2. What is the player?

The campaign opening establishes the player as a **Commander** already embedded in a faction headquarters, but the player's origin, prior history, faction identity, and path into command remain to be defined.

How did the player become Commander?

What faction do they represent?

What relationship do they have with the Empress?

What was happening immediately before the opening scene?

### 3. What makes a soldier matter?

What persists about an individual?

What can make one soldier memorable?

### 4. What does death mean?

Campaign death is an immediate loss of the current attempt. The player is not expected to survive every encounter, but the design expectation is that they **should not die**.

What persists after a campaign loss?

What does a later checkpoint preserve?

How do death and replacement differ between campaign, cooperative, and persistent modes?

### 5. What makes a universe end?

Can a faction actually win?

Can a universe reach peace?

Can it collapse?

Can it be restarted?

### 6. How autonomous is the war?

What happens when no player is looking?

Do armies move?

Do researchers continue?

Do factories produce?

Do enemies adapt?

### 7. What is the technology ladder?

What can be researched?

How does a new technology move from research to demonstration to qualification to deployment?

### 8. What is command authority?

How does a commander's decision propagate through officers, squads, and individual soldiers?

How much can subordinates reinterpret orders?

### 9. What is the relationship between skill and qualification?

Is qualification a certification, a progression path, a permission system, or all three?

### 10. How large is the living simulation?

What population and simulation levels are authoritative? How are hundreds of thousands of visible soldiers represented while preserving individual identity and meaningful behavior?

### 11. What is the qualification graph?

Which qualifications exist? Which require other qualifications, rank, officer status, training facilities, or organizational need?

### 12. How do rules evolve?

Who can establish, modify, revoke, and enforce military procedures? Can organizations learn from failures and formalize new rules?

### 13. What does readiness mean?

How do staffing, training, fatigue, maintenance, supplies, and qualifications combine into operational readiness?

### 14. What is the personal economy?

What can soldiers own? What can they buy with raWWar digital currency? How are customized vehicles balanced against military equipment?

### 15. How alive is the world when unobserved?

Which systems continue continuously, which are simulated at lower fidelity, and which are abstracted until observation makes detail necessary?

### 16. What is the first ten minutes?

The opening campaign sequence is now defined at a high level: moniker, lightning, Imperial palace and living square, observation-driven exterior persistence, transition into the faction headquarters, Imperial soldiers ordering the Commander to report to the Empress, faction offices and dogma available for exploration, and an early assassination lesson for failure to obey.

The opening should demonstrate the philosophy of raWWar before explaining it.

What happens immediately after the player obeys and goes to the Empress?

What is the first actual mission?

What does the player know about the factions and the war at that point?


### 17. What is the player equipment progression?

What is the canonical entry-level exoskeleton?

Which additions are military issue, earned, purchased, discovered, or persistent-universe exclusive?

How much customization is available in campaign, cooperative campaign, and persistent play?

How does equipment affect qualification, readiness, maintenance, power, and identity?

### 18. How large is cooperative campaign?

What scenarios support two players? What changes at 8, 32, 64, or approximately 128 players?

Can cooperating players belong to different factions?

How are objectives, information, command authority, and conflicting interests handled?

### 19. What is the raWWar visual language?

What makes a raWWar soldier, vehicle, facility, weapon, and faction recognizable as belonging to this universe?

Which elements are modular, procedural, hand-authored, or generated?

### 20. What is the content production pipeline?

How are original soldiers, vehicles, environments, animation, audio, and effects authored, validated, versioned, and delivered to the Experience and Workshop Renderer?

### 21. How is the Gesture system authored and integrated?

How is Gesture authored, represented, evaluated, and individualized? How is it integrated with the Renderer and MicroBundle Gesture Providers?

### 22. When does faction selection occur relative to the campaign opening?

The current documents describe two sequences that may conflict:

- The [Game Design Bible](GAME_DESIGN_BIBLE.md) opens with the player already serving as a Commander inside a faction headquarters. Imperial soldiers demand that the Commander report to the Empress immediately; the player has only a short window to comply, while faction offices can be explored.
- [Faction Design](FACTIONS.md) describes the player entering the headquarters, seeing thirteen physical doors, exploring faction offices, and choosing a faction before the choice resolves into starting relationships, territory, doctrine, personnel, and opening conditions.

These could be reconciled if the opening headquarters is a pre-choice selection space, if faction selection happens before the campaign opening, or if the doors serve another purpose. The current documents do not establish which interpretation is intended. Do not choose one silently.

**Creator decision needed:** Is the thirteen-door choice made before the moniker/lightning opening, during the headquarters scene before the Empress's order, after the opening order, or in another sequence? How can that choice coexist with the established Commander identity and the short compliance deadline?
