   M S B A M L   `   `   `  ���� N  JInstagram Assistant, Version=1.0.6.7, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Instagram_Assistant.ViewModelInstagram_Assistant.ViewModel  C&clr-namespace:Instagram_Assistant.ViewInstagram_Assistant.View  (    "Instagram_Assistant.MainWindowView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     B
viewmodels+clr-namespace:Instagram_Assistant.ViewModel     8views&clr-namespace:Instagram_Assistant.View     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/    � Title$  My Insty��$	��600��$
��1000�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�   	PageColor#���    � WindowState �� *CurWindowState+ � DataContext 7   1Instagram_Assistant.ViewModel.MainWindowViewModel   � 	Resources  �� DataType5   /Instagram_Assistant.ViewModel.MainPageViewModel+   %Instagram_Assistant.View.MainPageView6   0Instagram_Assistant.ViewModel.LoginPageViewModel,   &Instagram_Assistant.View.LoginPageView:   4Instagram_Assistant.ViewModel.DashboardPageViewModel0   *Instagram_Assistant.View.DashboardPageView9  3Instagram_Assistant.ViewModel.SettingsPageViewModel/	   )Instagram_Assistant.View.SettingsPageView5
   /Instagram_Assistant.ViewModel.LogsPageViewModel+   %Instagram_Assistant.View.LogsPageView8  2Instagram_Assistant.ViewModel.AccountPageViewModel.   (Instagram_Assistant.View.AccountPageView6  0Instagram_Assistant.ViewModel.AboutPageViewModel,   &Instagram_Assistant.View.AboutPageView<  6Instagram_Assistant.ViewModel.FeedStoriesPageViewModel7   1Instagram_Assistant.View.FeedStoriesWatchPageView9  3Instagram_Assistant.ViewModel.UnfollowPageViewModel/   )Instagram_Assistant.View.UnfollowPageView9  3Instagram_Assistant.ViewModel.AudiencePageViewModel8  2Instagram_Assistant.ViewModel.GeoLikePageViewModel.   (Instagram_Assistant.View.GeoLikePageView<  6Instagram_Assistant.ViewModel.HashtagLikePageViewModel2   ,Instagram_Assistant.View.HashtagLikePageView  sbAlertShow �� TargetProperty	 t� From	
 t� To �� 
TargetName g� AccelerationRatio g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness  sbAlertHide��%f  (��       *, +)(��      *, +)(�� ?     *, +)(�� c     *, +)(�� �     *,
 +)(�� �     *, +)(�� �     *, +)(�� �     *, +)(��     *, +)(�� (    *, +)(�� K    *, +)(�� m    *, +)&	 �    &	     �� mainView��"  .R�  �� 	loginView��"  .R�  �� dashboardView��"  .R�  �� settingsView��"  .R�	  �� logsView��" 
 .R�  �� accView��"  .R�  �� 	aboutView��"  .R�  �� feedStories��"  .R�  �� unfollowView��"  .R�  �� audienceView��"  .R�  �� geoLikeView��"  .R�  �� hastagLikeView��"  .R�  �� .��t� $ Margin�$	 	0,-90,0,0X�$
 0,8,0,0X�$ 	AlertGrid��$ .9Q�$ 0:0:1.5D�   $ EaseOut=�$ 2��$ 6Q��� .��t� $ Margin�$	 0,8,0,0X�$
 	0,-90,0,0X�$ 	AlertGrid��$ 0:0:1D�   $ EaseIn=�$ 1��$ 3Q�.��� .+�� -    �Name$ mainGrid��$y�Visible=� � RowDefinitions �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� ��  � ColumnDefinitions �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��4��$��20��$��0��  TopBorderColor#���   J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 1Q�   ShadowDepth$	 0.2Q�  ShadowBrush   Color# �     RenderingBias$ Quality=��� -    ��Name$ DragMoveBorder��$��30��$��1��
������� J� Opacity$ 0.00001Q��� -    ��Name$
 Menu��$��3��$��10��$��4��
������   MenuListBoxStyle#���  $ �� IsSynchronizedWithCurrentItem$
 TrueY� �� SelectedIndex ��  �� Path$ MenuItem�  �� Mode$  TwoWay=�! �� UpdateSourceTrigger$! PropertyChanged=�@���   MenuListBoxItemStyle#���  ���� " �� Orientation$" 
Horizontal=�.+�!  FontAwesome.WPF.FontAwesome  #   Icon$
# User=�$��25(�$��25��$��0,0,8,0q��� $��Account��$��Center=�$��12(��� #���  ���� $" 
Horizontal=�.+�  $# 
Tachometer=�$��25(�$��25��$��0,0,8,0q��� $��	Dashboard��$��Center=�$��12(��� #���  ���� $" 
Horizontal=�.+�  $	# Cog=�$��25(�$��25��$��0,0,8,0q��� $��Settings��$��Center=�$��12(��� #���  ���� $" 
Horizontal=�.+�  $# NewspaperOutline=�$��22(�$��25��$��0,0,8,0q��� $
��Logs��$��Center=�$��12(��� #���  ���� $" 
Horizontal=�.+�  $# QuestionCircle=�$��22(�$��25��$��0,0,8,0q��� $��About��$��Center=�$��12(�$ � Triggers$ :� % :� RoutedEvent$% ListBox.SelectionChanged���� -   & ��Name$& CloseApp��$��27��$��15(�  FontAwesome.WPF.Awesome'   Content$' Close=�
������ $��0q�
����������� *CloseAppCommand+�� -   $& CollapseWindow��$��15(�
������ $��0q�
�������$��26��$' WindowMinimize=����� *CollapseCommand+�� $��0��$��2��$��0��$��4��$>��8pack://application:,,,/Resources/Images/insta-splash.png��$��70���� -   ( ��Name$( ContControl��$��0��$��1��$��17��$��28������ *SelectedViewModel+�� -   $( MainControl��$��4��$��1��$��17��$��24������ *MainSelectedViewModel+� -   $ 	AlertGrid��$��	0,-70,0,0q�$	��Top=�$	��450��$��90�� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� ��  �� $��5 ��� $
��auto ��� $��5 ��� $��10 ��� $
��auto ��� $��10 �.+���   FirstColorBrush#���  $	 0.9Q�$��25��$��8��
�������) �� CornerRadius$) 10����   SecondColorBrush#���  $	 0.4Q�$��25��$��3��
�������$) 	10,10,0,0���� $��Center=�
�������$��1��$��4��$��20������ *NewAlert.Time+�� $��1��$��1��$��12��$" 
Horizontal=�$��Center=�.+�"  FontAwesome.WPF.ImageAwesome  $��20��*   
Foreground
* �����+   Icon+ �� *NewAlert.AlertImage+�� $��15,0,0,0q�$
��Left=�$��Center=�$��Center=� 	 Big#��� 	 
����������� *NewAlert.AlertFrom+�� $��1��$��20��$��4��$
��Wrap=�$��Center=� 	
 Small#��� 
 
�������, �� LineStackingStrategy$, BlockLineHeight=�- �� 
LineHeight$- 15������ *NewAlert.AlertMessage+*  $Microsoft.Xaml.Behaviors.Interaction.   Triggers. /   )Microsoft.Xaml.Behaviors.Core.DataTrigger   /    Value$/ ON��:!  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger0 !  Binding0 �� *	IsAlertOn+*"  $Microsoft.Xaml.Behaviors.TriggerBase1 "  Actions.1 <#  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction#  5$  /Microsoft.Xaml.Behaviors.Media.StoryboardAction2 $  
Storyboard#2 [ 3 #  ControlStoryboardOption$
3 Play=�   $	/ OFF��0 �� *	IsAlertOn+.1 #  #2 [ $
3 Play=�