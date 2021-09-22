
import pygame

if __name__ == '__main__':
    pygame.init()
    clock = pygame.time.Clock()

    joysticks = [pygame.joystick.Joystick(i) for i in range(pygame.joystick.get_count())]
    for joy in joysticks:
        joy.init()

    while (True):
        events = pygame.event.get()
        for event in events:
            if event.type == pygame.JOYBUTTONDOWN:
                if joysticks[0].get_button(2):
                    print("Button 2 Pressed")
            elif event.type == pygame.JOYBUTTONUP:
                if joysticks[0].get_button(2):
                    print("Button 2 Released")
        clock.tick(20)

    pygame.quit()
