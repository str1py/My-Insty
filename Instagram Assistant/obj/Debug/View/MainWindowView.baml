   M S B A M L   `   `   `  ����N  JInstagram Assistant, Version=1.0.6.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Instagram_Assistant.ViewModelInstagram_Assistant.ViewModel  5   	   C&clr-namespace:Instagram_Assistant.ViewInstagram_Assistant.View  5   	   (    "Instagram_Assistant.MainWindowView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   B
viewmodels+clr-namespace:Instagram_Assistant.ViewModel     5   	   8views&clr-namespace:Instagram_Assistant.View     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5	   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5
   	     � Title$  My Insty��5   	   $	��600��6   $
��1000��6'    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.    � 
ResizeMode$ NoResize=�5   	      	PageColor#���   6     � WindowState 6I   �� *CurWindowState+ � DataContext 5      7   1Instagram_Assistant.ViewModel.MainWindowViewModel  5   
    � 	Resources 5       �� DataType5   /Instagram_Assistant.ViewModel.MainPageViewModel+   %Instagram_Assistant.View.MainPageView6   0Instagram_Assistant.ViewModel.LoginPageViewModel,   &Instagram_Assistant.View.LoginPageView:   4Instagram_Assistant.ViewModel.DashboardPageViewModel0   *Instagram_Assistant.View.DashboardPageView9  3Instagram_Assistant.ViewModel.SettingsPageViewModel/	   )Instagram_Assistant.View.SettingsPageView5
   /Instagram_Assistant.ViewModel.LogsPageViewModel+   %Instagram_Assistant.View.LogsPageView8  2Instagram_Assistant.ViewModel.AccountPageViewModel.   (Instagram_Assistant.View.AccountPageView6  0Instagram_Assistant.ViewModel.AboutPageViewModel,   &Instagram_Assistant.View.AboutPageView<  6Instagram_Assistant.ViewModel.FeedStoriesPageViewModel7   1Instagram_Assistant.View.FeedStoriesWatchPageView9  3Instagram_Assistant.ViewModel.UnfollowPageViewModel/   )Instagram_Assistant.View.UnfollowPageView9  3Instagram_Assistant.ViewModel.AudiencePageViewModel8  2Instagram_Assistant.ViewModel.GeoLikePageViewModel.   (Instagram_Assistant.View.GeoLikePageView<  6Instagram_Assistant.ViewModel.HashtagLikePageViewModel2   ,Instagram_Assistant.View.HashtagLikePageView  sbAlertShow �� TargetProperty	 t� From	
 t� To �� 
TargetName g� AccelerationRatio g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness  sbAlertHide��%+  (��       5   )   *, +)(�� ?     5   *   *, +)(��      5   .   *, +)(�� �     5   -   *, +)(��     5!   )   *,
 +)(�� E    5$   (   *, +)(�� �    5'   *   *, +)(�� �    5*   ,   *, +)(��     5-   -   *, +)(�� H    50   -   *, +)(�� �    53   ,   *, +)(�� �    56   /   *, +)&	     &	     �� 5   
   mainView��"  6)   .R�5        5      �� 5   
   	loginView��"  6*   .R�5        5      �� 5   
   dashboardView��"  6.   .R�5        5      �� 5   
   settingsView��"  6-   .R�5      	  5       �� 5!   
   logsView��" 
 6)   .R�5"        5#      �� 5$   
   accView��"  6(   .R�5%        5&      �� 5'   
   	aboutView��"  6*   .R�5(        5)      �� 5*   
   feedStories��"  6,   .R�5+        5,      �� 5-   
   unfollowView��"  6-   .R�5.        5/      �� 50   
   audienceView��"  6-   .R�51        52      �� 53   
   geoLikeView��"  6,   .R�54        55      �� 56   
   hastagLikeView��"  6/   .R�57        58      �� 5:   
   .��5;      t� $ Margin�6!   $	 	0,-90,0,0X�5<   !   $
 0,8,0,0X�62   $ 	AlertGrid��5=   !   $ .9Q�5>   !   $ 0:0:1.5D�68    5?        5@      $ EaseOut=�6"   $ 2��67   $ 6Q�6H   5B      5C      �� 5D   
   .��5E      t� $ Margin�6!   $	 0,8,0,0X�5F   !   $
 	0,-90,0,0X�60   $ 	AlertGrid��5G   !   $ 0:0:1D�5H   !    5I        5J      $ EaseIn=�6"   $ 1��66   $ 3Q�6G   5L      5N      5O      .��5Q      � .+�5R   
   � -   5         �Name$ mainGrid��5R      $y�Visible=�6!    � RowDefinitions 5S      �� 5T      �� 5U      �� 5V      �� 5W      �� 5X      �� 5Y      �� 5Z      �� 5[      �� 5\      �� 5]      �� 5^      �� 5_      �� 5`      �� 5a      �� 5b      �� 5c      �� 5d      �� 5e       � ColumnDefinitions 5g      �� 5h      �� 5i      �� 5j      �� 5k      �� 5l      �� 5m      �� 5n      �� 5o      �� 5p      �� 5q      �� 5r      �� 5s      �� 5t      �� 5u      �� 5v      �� 5w      �� 5x      �� 5y      �� 5z      �� 5{      �� 5|      �� 5}      �� 5~      �� 5      �� 5�      �� 5�      �� 5�      �� 5�      .+�5�      �� $��4��6   $��20��6)   $��0��6;     TopBorderColor#���  6I    J� Effect 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�         
BlurRadius$ 1Q�6'      ShadowDepth$	 0.2Q�66     ShadowBrush   Color# �  6H      RenderingBias$ Quality=�6n   6�   5�      �� 5�      -   5         ��Name$ DragMoveBorder��5�      $��30��6-   $��1��6B   
������� J� Opacity$ 0.00001Q�5�      5�      �� 5�      -   5         ��Name$
 Menu��5�      $��3��5�      $��10��6#   $��4��65   
������   MenuListBoxStyle#���  5�      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5�       �� SelectedIndex 5�      ��  �� Path$ MenuItem�  �� Mode$  TwoWay=�! �� UpdateSourceTrigger$! PropertyChanged=�@�5�      �� 5�        MenuListBoxItemStyle#���  6"   ��5�      �� 5�      " �� Orientation$" 
Horizontal=�6)   .+�5�   "   !  FontAwesome.WPF.FontAwesome  #   Icon$
# User=�61   $��25(�5�   1   $��25��5�   1   $��0,0,8,0q�5�   1   �� 5�   "   $��Account��6,   $��Center=�5�   ,   $��12(�5�   ,   5�      5�      �� 5�      #���  6"   ��5�      �� 5�      $" 
Horizontal=�6)   .+�5�   "     $# 
Tachometer=�61   $��25(�5�   1   $��25��5�   1   $��0,0,8,0q�5�   1   �� 5�   "   $��	Dashboard��6,   $��Center=�5�   ,   $��12(�5�   ,   5�      5�      �� 5�      #���  6"   ��5�      �� 5�      $" 
Horizontal=�6)   .+�5�   "     $	# Cog=�61   $��25(�5�   1   $��25��5�   1   $��0,0,8,0q�5�   1   �� 5�   "   $��Settings��6,   $��Center=�5�   ,   $��12(�5�   ,   5�      5�      �� 5�      #���  6"   ��5�      �� 5�      $" 
Horizontal=�6)   .+�5�   "     $# NewspaperOutline=�61   $��22(�5�   1   $��25��5�   1   $��0,0,8,0q�5�   1   �� 5�   "   $
��Logs��6,   $��Center=�5�   ,   $��12(�5�   ,   5�      5�      �� 5�      #���  6"   ��5�      �� 5�      $" 
Horizontal=�6)   .+�5�   "     $# QuestionCircle=�61   $��22(�5�   1   $��25��5�   1   $��0,0,8,0q�5�   1   �� 5�   "   $��About��6,   $��Center=�5�   ,   $��12(�5�   ,   5�      5�      $ � Triggers$ 5�      :� 5�      % :� RoutedEvent$% ListBox.SelectionChanged��6#   5�      5�      �� 5�      -   5        & ��Name$& CloseApp��5�      $��27��5�      $��15(�6&     FontAwesome.WPF.Awesome'   Content$' Close=�65   
������ $��0q�5�   .   
���������5�      �� *CloseAppCommand+5�      �� 5�      -   5        $& CollapseWindow��5�      $��15(�6-   
������ $��0q�5�   .   
�������$��26��5�      $' WindowMinimize=�6&   ��5�      �� *CollapseCommand+6;   �� 5�      $��0��6   $��2��6!   $��0��62   $��4��6C   $>��8pack://application:,,,/Resources/Images/insta-splash.png��5�      $��70��6W   �� 5�      -   5        ( ��Name$( ContControl��5�      $��0��5�      $��1��6/   $��17��6=   $��28��6O   ��5�      �� *SelectedViewModel+�� 5�      -   5        $( MainControl��5�      $��4��5�      $��1��6/   $��17��6<   $��24��6N   ��5�      �� *MainSelectedViewModel+5�      � 5�   
   -   5        $ 	AlertGrid��5�      $��	0,-70,0,0q�6"   $	��Top=�65   $	��450��6M   $��90��6Y    5�      �� 5      �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5	     �� 5
     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5      5     �� 5     $��5 �6    �� 5     $
��auto �6    �� 5     $��5 �6    �� 5     $��10 �6    �� 5     $
��auto �6    �� 5      $��10 �6    .+�5"     ��   FirstColorBrush#���  6   $	 0.9Q�6D   $��25��6R   $��8��6g   
�������) �� CornerRadius$) 10��5#  )   6=   �� 5$       SecondColorBrush#���  6   $	 0.4Q�6E   $��25��6S   $��3��6i   
�������$) 	10,10,0,0��5%  )   6D   �� 5'     $��Center=�6   
�������$��1��5)     $��4��6!   $��20��65   ��5(     �� *NewAlert.Time+5)  H   �� 5*     $��1��6   $��1��6)   $��12��67   $" 
Horizontal=�6L   $��Center=�6e   .+�5+     "  FontAwesome.WPF.ImageAwesome  $��20��6"   *   
Foreground
* �����+   Icon+ 6-   �� *NewAlert.AlertImage+6g   �� 5,     $��15,0,0,0q�6   $
��Left=�5-     $��Center=�60   $��Center=�6K    	 Big#��� 	 5.     
���������5/     �� *NewAlert.AlertFrom+6=   50     �� 52     $��1��6   $��20��6(   $��4��6>   $
��Wrap=�53     $��Center=�6,    	
 Small#��� 
 54     
�������, �� LineStackingStrategy$, BlockLineHeight=�55     - �� 
LineHeight$- 15��6?   ��56     �� *NewAlert.AlertMessage+6A   *  $Microsoft.Xaml.Behaviors.Interaction.   Triggers. 59     /   )Microsoft.Xaml.Behaviors.Core.DataTrigger   5:     /    Value$/ ON��6>   :!  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger0 !  Binding0 6    �� *	IsAlertOn+*"  $Microsoft.Xaml.Behaviors.TriggerBase1 "  Actions.1 5;     <#  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction#  5$  /Microsoft.Xaml.Behaviors.Media.StoryboardAction2 $  
Storyboard#2 [ 60   3 #  ControlStoryboardOption$
3 Play=�6[   5<        5=     $	/ OFF��6>   0 6    �� *	IsAlertOn+.1 5>     #  #2 [ 60   $
3 Play=�6\   5?     5B     5C     5D     